import { api } from "../lib/api";
import { ENDPOINTS } from "../lib/endpoints";

const MOCK_QUICK_ACTIONS = [
    {
        icon: 'ri-exchange-line',
        label: 'Transferir Dinero',
        variant: 'Primary',
        href: '/user/transferencias'
    },
    // {
    //     icon: 'ri-wallet-line',
    //     label: 'Consultar Saldo',
    //     variant: 'Secondary',
    //     href: '/user/saldo'
    // },
    {
        icon: 'ri-history-line',
        label: 'Ver Historial',
        variant: 'Neutral',
        href: '/user/historial'
    }
];

export const userService = {
    // Aggregates data from multiple endpoints to satisfy Dashboard requirements
    getDashboardData: async () => {
        const userId = typeof window !== 'undefined' ? localStorage.getItem('userId') : null;
        if (!userId) {
            console.error("User authenticated but userId missing.");
            // Potentially redirect or handle gracefully
        }

        // 1. Get Accounts
        let cards: any[] = [];
        try {
            if (userId) cards = await api.get<any[]>(ENDPOINTS.CUENTAS.BY_SOCIO(userId));
        } catch (e) {
            console.error("Error fetching accounts", e);
        }

        // 2. Get Recent Transactions 
        let transactions: any[] = [];
        let totalIncome = 0;
        let totalExpense = 0;

        if (cards && cards.length > 0) {
            const firstAccountId = (cards[0] as any).id;
            if (firstAccountId) {
                try {
                    transactions = await api.get<any[]>(ENDPOINTS.MOVIMIENTOS.BY_ACCOUNT(firstAccountId));

                    // Calculate simplified stats from recent transactions
                    transactions.forEach((t: any) => {
                        const amount = parseFloat(t.amount || 0);
                        // Heuristic to determine type if explicit type is missing
                        // Adjust based on actual backend response
                        if (amount > 0) {
                            totalIncome += amount;
                        } else {
                            totalExpense += Math.abs(amount);
                        }
                    });
                } catch (e) {
                    console.error("Error fetching transactions", e);
                }
            }
        }

        // Dynamic Stats based on fetched data
        const stats = [
            {
                title: 'Ingresos',
                amount: `€${totalIncome.toFixed(2)}`,
                percentage: '+0.0%', // Placeholder calculation
                trend: 'up',
                icon: 'ri-arrow-down-line',
                variant: 'success'
            },
            {
                title: 'Gastos',
                amount: `€${totalExpense.toFixed(2)}`,
                percentage: '-0.0%',
                trend: 'down',
                icon: 'ri-arrow-up-line',
                variant: 'danger'
            },
            {
                title: 'Ahorro',
                amount: `€${(totalIncome - totalExpense).toFixed(2)}`,
                percentage: '+0.0%',
                trend: 'up',
                icon: 'ri-safe-line',
                variant: 'info'
            }
        ];

        return {
            cards,
            stats,
            transactions: transactions.slice(0, 5),
            quickActions: MOCK_QUICK_ACTIONS
        };
    },

    getTransactions: async () => {
        const userId = typeof window !== 'undefined' ? localStorage.getItem('userId') : null;
        if (!userId) return [];

        try {
            const cards = await api.get<any[]>(ENDPOINTS.CUENTAS.BY_SOCIO(userId));
            if (cards && cards.length > 0) {
                const firstAccountId = (cards[0] as any).id;
                return api.get<any[]>(ENDPOINTS.MOVIMIENTOS.BY_ACCOUNT(firstAccountId));
            }
        } catch (e) {
            console.error("Error fetching transactions details", e);
        }
        return [];
    },

    getContacts: async () => {
        return api.get<any[]>('/user/contacts'); // Keeping mock endpoint for contacts
    }
};
