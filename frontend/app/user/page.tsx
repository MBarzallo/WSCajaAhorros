"use client"
import { useState, useEffect } from 'react';
import styles from '../ui/stylesheets/Dashboard.module.css'
import { BankCard } from '../components/dashboard/BankCard';
import { QuickAction } from '../components/dashboard/QuickAction';
import { StatCard } from '../components/dashboard/StatCard';
import { Transaction } from '../components/dashboard/Transaction';
import { userService } from '../services/user.service';

export default function Dashboard() {
  const [data, setData] = useState<any>(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const dashboardData = await userService.getDashboardData();
        setData(dashboardData);
      } catch (error) {
        console.error("Error fetching dashboard data", error);
      } finally {
        setLoading(false);
      }
    };
    fetchData();
  }, []);

  if (loading) {
    return <div className={styles.container}><p>Cargando datos...</p></div>;
  }

  if (!data) {
    return <div className={styles.container}><p>Error al cargar datos.</p></div>;
  }

  return (
    <>
      <link
        href="https://cdn.jsdelivr.net/npm/remixicon@3.5.0/fonts/remixicon.css"
        rel="stylesheet"
      />

      <div className={styles.container}>
        {/* Header */}
        <div className={styles.header}>
          <h1 className={styles.title}>Panel de Control</h1>
          <p className={styles.subtitle}>Bienvenido, Juan Pérez</p>
        </div>

        {/* Tarjetas y Acciones Rápidas */}
        <div className={styles.gridMain}>
          {data.cards?.map((card: any, index: number) => (
            <BankCard key={index} {...card} />
          ))}

          <div className={styles.quickActionsCard}>
            <div className={styles.quickActionsHeader}>
              <h3 className={styles.quickActionsTitle}>Acciones Rápidas</h3>
            </div>
            <div className={styles.quickActionsList}>
              {data.quickActions?.map((action: any, index: number) => (
                <QuickAction key={index} {...action} />
              ))}
            </div>
          </div>
        </div>

        {/* Estadísticas */}
        <div className={styles.gridStats}>
          {data.stats?.map((stat: any, index: number) => (
            <StatCard key={index} {...stat} />
          ))}
        </div>

        {/* Transacciones Recientes */}
        <div className={styles.transactionsCard}>
          <div className={styles.transactionsHeader}>
            <h2 className={styles.transactionsTitle}>Transacciones Recientes</h2>
            <a href="/user/historial" className={styles.viewAllLink}>
              Ver todas
            </a>
          </div>
          <div className={styles.transactionsList}>
            {data.transactions?.map((transaction: any, index: number) => (
              <Transaction key={index} {...transaction} />
            ))}
          </div>
        </div>
      </div>
    </>
  );
};