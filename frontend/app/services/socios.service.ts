import { api } from "../lib/api";
import { ENDPOINTS } from "../lib/endpoints";

export const sociosService = {
  getAll: (filters?: {
    identificacion?: string;
    nombres?: string;
    activo?: boolean;
  }) => {
    const params = new URLSearchParams();

    if (filters?.identificacion)
      params.append("identificacion", filters.identificacion);

    if (filters?.nombres)
      params.append("nombres", filters.nombres);

    if (filters?.activo !== undefined)
      params.append("activo", String(filters.activo));

    const query = params.toString();
    return api.get(`${ENDPOINTS.SOCIOS.GET_ALL}${query ? `?${query}` : ""}`);
  },

  getById: (id: string) => {
    return api.get(ENDPOINTS.SOCIOS.GET_BY_ID(id));
  },

  create: (data: any) => {
    return api.post(ENDPOINTS.SOCIOS.CREATE, data);
  },

  update: (id: string, data: any) => {
    return api.put(ENDPOINTS.SOCIOS.UPDATE(id), data);
  },

  activate: (id: string) => {
    return api.put(ENDPOINTS.SOCIOS.ACTIVATE(id));
  },

  deactivate: (id: string) => {
    return api.put(ENDPOINTS.SOCIOS.DEACTIVATE(id));
  },
};
