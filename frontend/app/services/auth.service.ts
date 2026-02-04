import { api } from "../lib/api";
import { ENDPOINTS } from "../lib/endpoints";

export const authService = {
    login: async (credentials: any) => {
        return api.post(ENDPOINTS.AUTH.LOGIN, credentials);
    },

    register: async (data: any) => {
        return api.post(ENDPOINTS.USUARIOS.CREATE, data);
    }
};
