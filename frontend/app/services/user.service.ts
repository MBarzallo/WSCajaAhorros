import { api } from "../lib/api";
import { ENDPOINTS } from "../lib/endpoints";

export const usuariosService = {
  getAll: () => api.get(ENDPOINTS.USUARIOS.GET_ALL),

  create: (data: {
    nombreUsuario: string;
    correoElectronico: string;
  }) => api.post(ENDPOINTS.USUARIOS.CREATE, data),

  activar: (id: string) =>
    api.put(`/api/Usuarios/${id}/activar`, {}),

  desactivar: (id: string) =>
    api.put(`/api/Usuarios/${id}/desactivar`, {}),
};
