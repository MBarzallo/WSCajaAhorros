export const ENDPOINTS = {
    AUTH: {
        LOGIN: '/api/Autenticacion/login',
        ACTIVATE: (id: string) => `/api/Autenticacion/activar/${id}`,
        DEACTIVATE: (id: string) => `/api/Autenticacion/desactivar/${id}`,
    },
    USUARIOS: {
        GET_ALL: '/api/Usuarios',
        CREATE: '/api/Usuarios', // Register
        ASSIGN_ROLE: (id: string) => `/api/Usuarios/rol/${id}`,
    },
    SOCIOS: {
        CREATE: '/api/Socios',
    },
    CUENTAS: {
        BY_SOCIO: (id: string | number) => `/api/Cuentas/socio/${id}`,
        CREATE: '/api/Cuentas',
        BLOCK: (id: string | number) => `/api/Cuentas/${id}/bloquear`,
        CLOSE: (id: string | number) => `/api/Cuentas/${id}/cerrar`,
    },
    MOVIMIENTOS: {
        DEPOSIT: '/api/Movimientos/deposito',
        WITHDRAW: '/api/Movimientos/retiro',
        BY_ACCOUNT: (id: string | number) => `/api/Movimientos/cuenta/${id}`,
    },
    TRANSFERENCIAS: {
        CREATE: '/api/Transferencias',
    },
    OPERACIONES: {
        DEPOSIT: '/api/Operaciones/depositar',
        WITHDRAW: '/api/Operaciones/retirar',
    },
    PRODUCTOS: {
        GET_ALL: '/api/ProductosCuenta',
    },
    ROLES: {
        GET_ALL: '/api/Roles',
    },
};
