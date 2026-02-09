import { api } from "../lib/api";
import { ENDPOINTS } from "../lib/endpoints";

export const movimientosService = {
 depositar: (data: {
    cuentaId: string;
    monto: number;
    descripcion: string;
  }) => {
    return api.post(ENDPOINTS.MOVIMIENTOS.DEPOSIT, data);
  },

    retirar: (data: {
        cuentaId: string;
        monto: number;
        descripcion: string;
    }) => {
        return api.post(ENDPOINTS.MOVIMIENTOS.WITHDRAW, data);
    },
  getByCuenta: (cuentaId: number) => {
    return api.get(ENDPOINTS.MOVIMIENTOS.BY_ACCOUNT(cuentaId));
  },
};
