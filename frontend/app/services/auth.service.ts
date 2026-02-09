import { api } from "../lib/api";
import { ENDPOINTS } from "../lib/endpoints";

export const authService = {
  login: (credentials: {
    nombreUsuario: string;
    contrasena: string;
  }) => {
    return api.post(ENDPOINTS.AUTH.LOGIN, credentials);
  },

  activateUser: (userId: string) => {
    return api.put(ENDPOINTS.AUTH.ACTIVATE(userId));
  },

  deactivateUser: (userId: string) => {
    return api.put(ENDPOINTS.AUTH.DEACTIVATE(userId));
  },
};
