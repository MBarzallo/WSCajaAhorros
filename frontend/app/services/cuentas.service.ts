import { api } from "../lib/api";
import { ENDPOINTS } from "../lib/endpoints";

export const cuentasService = {
  getBySocio: (socioId: string) => {
    return api.get(ENDPOINTS.CUENTAS.BY_SOCIO(socioId));
  },

  create: (data: {
    socioId: string;
    productoCuentaId: string;
  }) => {
    return api.post(ENDPOINTS.CUENTAS.CREATE, data);
  },

  bloquear: (cuentaId: string) => {
    return api.put(ENDPOINTS.CUENTAS.BLOCK(cuentaId));
  },

  cerrar: (cuentaId: string) => {
    return api.put(ENDPOINTS.CUENTAS.CLOSE(cuentaId));
  },
};

export const productosCuentaService = {
  getAll: () => {
    return api.get(ENDPOINTS.PRODUCTOS.GET_ALL);
  },
};
