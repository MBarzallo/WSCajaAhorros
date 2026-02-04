
import { ENDPOINTS } from './endpoints';

const BASE_URL = process.env.NEXT_PUBLIC_API_URL || 'http://localhost:5070/api';

class ApiClient {
    private getAuthToken(): string | null {
        if (typeof window !== 'undefined') {
            return localStorage.getItem('token');
        }
        return null;
    }

    private async request<T>(endpoint: string, options: RequestInit = {}): Promise<T> {
        const url = `${BASE_URL}${endpoint}`;

        const headers: Record<string, string> = {
            'Content-Type': 'application/json',
            ...(options.headers as Record<string, string>),
        };

        const token = this.getAuthToken();
        if (token) {
            headers['Authorization'] = `Bearer ${token}`;
        }

        console.log(`[API] ${options.method || 'GET'} ${url}`);

        try {
            const response = await fetch(url, { ...options, headers });

            if (!response.ok) {
                // Handle 401 Unauthorized
                if (response.status === 401) {
                    if (typeof window !== 'undefined') {
                        localStorage.removeItem('token');
                        window.location.href = '/auth/login';
                    }
                }

                let errorMessage = response.statusText;
                try {
                    const errorBody = await response.json();
                    errorMessage = errorBody.message || errorMessage;
                } catch (e) {
                    // response is not json
                }
                throw new Error(`API Error ${response.status}: ${errorMessage}`);
            }

            // Check if response has content
            const contentType = response.headers.get("content-type");
            if (contentType && contentType.indexOf("application/json") !== -1) {
                return response.json();
            } else {
                return {} as T; // Return empty object if no JSON content (e.g. 204 No Content, or plain text)
            }

        } catch (error) {
            console.error("API Request Failed:", error);
            throw error;
        }
    }

    get<T>(endpoint: string) {
        return this.request<T>(endpoint, { method: 'GET' });
    }

    post<T>(endpoint: string, body: any) {
        return this.request<T>(endpoint, {
            method: 'POST',
            body: JSON.stringify(body),
        });
    }

    put<T>(endpoint: string, body: any) {
        return this.request<T>(endpoint, {
            method: 'PUT',
            body: JSON.stringify(body),
        });
    }

    delete<T>(endpoint: string) {
        return this.request<T>(endpoint, { method: 'DELETE' });
    }
}

export const api = new ApiClient();
