import { api } from "../lib/api";
import { ENDPOINTS } from "../lib/endpoints";

export const transferenciasService = {
  transferir: (data: {
    cuentaOrigenId: string;
    cuentaDestinoId: string;
    monto: number;
    observacion: string;
  }) => {
    return api.post(ENDPOINTS.TRANSFERENCIAS.CREATE, data);
  },
};
