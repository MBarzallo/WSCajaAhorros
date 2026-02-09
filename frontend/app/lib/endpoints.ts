export const ENDPOINTS = {
  AUTH: {
    LOGIN: "/api/Autenticacion/login",
    ACTIVATE: (id: string) => `/api/Autenticacion/activar/${id}`,
    DEACTIVATE: (id: string) => `/api/Autenticacion/desactivar/${id}`,
  },
  USUARIOS: {
    GET_ALL: "/api/Usuarios",
    CREATE: "/api/Usuarios", // Register
    ASSIGN_ROLE: (id: string) => `/api/Usuarios/rol/${id}`,
  },
  SOCIOS: {
    GET_ALL: "/api/Socios",
    GET_BY_ID: (id: string) => `/api/Socios/${id}`,
    CREATE: "/api/Socios",
    UPDATE: (id: string) => `/api/Socios/${id}`,
    ACTIVATE: (id: string) => `/api/Socios/${id}/activar`,
    DEACTIVATE: (id: string) => `/api/Socios/${id}/desactivar`,
  },

  CUENTAS: {
  BY_SOCIO: (socioId: string) => `/api/Cuentas/socio/${socioId}`,
  CREATE: '/api/Cuentas',
  BLOCK: (cuentaId: string) => `/api/Cuentas/${cuentaId}/bloquear`,
  CLOSE: (cuentaId: string) => `/api/Cuentas/${cuentaId}/cerrar`,
},
PRODUCTOS: {
  GET_ALL: '/api/ProductosCuenta',
},
  MOVIMIENTOS: {
    DEPOSIT: "/api/Movimientos/deposito",
    WITHDRAW: "/api/Movimientos/retiro",
    BY_ACCOUNT: (id: string | number) => `/api/Movimientos/cuenta/${id}`,
  },
  TRANSFERENCIAS: {
    CREATE: "/api/Transferencias",
  },
  OPERACIONES: {
    DEPOSIT: "/api/Operaciones/depositar",
    WITHDRAW: "/api/Operaciones/retirar",
  },
  
  ROLES: {
    GET_ALL: "/api/Roles",
  },
};
