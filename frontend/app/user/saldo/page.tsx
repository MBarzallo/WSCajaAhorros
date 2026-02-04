// BalanceDashboard.tsx
'use client';

import React, { useState } from 'react';
import styles from '../../ui/stylesheets/Saldo.module.css';

// ==================== TIPOS ====================
interface Account {
  id: number;
  type: string;
  number: string;
  balance: number;
  gradientClass: 'gradientTeal' | 'gradientGreen' | 'gradientGray';
}

interface StatItem {
  label: string;
  value: string;
}

interface MetricItem {
  title: string;
  amount: string;
  date: string;
  type: 'income' | 'expense' | 'saving';
}

interface InsightItem {
  icon: string;
  title: string;
  description: string;
  gradientClass: 'gradientTealLight' | 'gradientGreenLight';
  iconBg: 'bgPrimary' | 'bgSuccess';
}

// ==================== COMPONENTES REUTILIZABLES ====================

// Componente: Botón de toggle para mostrar/ocultar
interface ToggleButtonProps {
  isVisible: boolean;
  onToggle: () => void;
}

export const ToggleButton: React.FC<ToggleButtonProps> = ({ isVisible, onToggle }) => {
  return (
    <button className={styles.toggleButton} onClick={onToggle}>
      <i className={isVisible ? 'ri-eye-line' : 'ri-eye-off-line'}></i>
    </button>
  );
};

// Componente: Stat Box (cajas pequeñas de estadísticas)
interface StatBoxProps {
  label: string;
  value: string;
}

export const StatBox: React.FC<StatBoxProps> = ({ label, value }) => {
  return (
    <div className={styles.statBox}>
      <p className={styles.statLabel}>{label}</p>
      <p className={styles.statValue}>{value}</p>
    </div>
  );
};

// Componente: Tarjeta de Balance Total
interface TotalBalanceCardProps {
  totalBalance: string;
  stats: StatItem[];
  showBalance: boolean;
  onToggleBalance: () => void;
}

export const TotalBalanceCard: React.FC<TotalBalanceCardProps> = ({
  totalBalance,
  stats,
  showBalance,
  onToggleBalance,
}) => {
  return (
    <div className={styles.totalBalanceCard}>
      <div className={styles.balanceHeader}>
        <div>
          <p className={styles.balanceLabel}>Saldo Total</p>
          <h2 className={styles.balanceAmount}>
            {showBalance ? totalBalance : '€••••••••'}
          </h2>
        </div>
        <ToggleButton isVisible={showBalance} onToggle={onToggleBalance} />
      </div>

      <div className={styles.statsGrid}>
        {stats.map((stat, index) => (
          <StatBox key={index} label={stat.label} value={stat.value} />
        ))}
      </div>
    </div>
  );
};

// Componente: Tarjeta de Cuenta Individual
interface AccountCardProps {
  account: Account;
  onViewDetails?: () => void;
}

export const AccountCard: React.FC<AccountCardProps> = ({ account, onViewDetails }) => {
  return (
    <div className={`${styles.accountCard} ${styles[account.gradientClass]}`}>
      <div className={styles.accountHeader}>
        <div>
          <p className={styles.accountType}>{account.type}</p>
          <p className={styles.accountNumber}>{account.number}</p>
        </div>
        <i className="ri-bank-card-2-line"></i>
      </div>

      <div className={styles.accountBalance}>
        <p className={styles.accountBalanceLabel}>Saldo Disponible</p>
        <p className={styles.accountBalanceAmount}>
          €{account.balance.toLocaleString('es-ES', { minimumFractionDigits: 2 })}
        </p>
      </div>

      <div className={styles.accountFooter}>
        <span className={styles.accountCurrency}>Moneda: EUR</span>
        <button className={styles.detailsButton} onClick={onViewDetails}>
          Ver Detalles
        </button>
      </div>
    </div>
  );
};

// Componente: Grid de Cuentas
interface AccountsGridProps {
  accounts: Account[];
  title?: string;
  onViewDetails?: (accountId: number) => void;
}

export const AccountsGrid: React.FC<AccountsGridProps> = ({
  accounts,
  title = 'Mis Cuentas',
  onViewDetails,
}) => {
  return (
    <div className={styles.section}>
      <h2 className={styles.sectionTitle}>{title}</h2>
      <div className={styles.accountsGrid}>
        {accounts.map((account) => (
          <AccountCard
            key={account.id}
            account={account}
            onViewDetails={() => onViewDetails?.(account.id)}
          />
        ))}
      </div>
    </div>
  );
};

// Componente: Toggle de Período
interface PeriodToggleProps {
  periods: string[];
  selectedPeriod: string;
  onPeriodChange: (period: string) => void;
}

export const PeriodToggle: React.FC<PeriodToggleProps> = ({
  periods,
  selectedPeriod,
  onPeriodChange,
}) => {
  return (
    <div className={styles.periodToggle}>
      {periods.map((period) => (
        <button
          key={period}
          className={`${styles.periodButton} ${
            selectedPeriod === period ? styles.periodButtonActive : ''
          }`}
          onClick={() => onPeriodChange(period)}
        >
          {period.charAt(0).toUpperCase() + period.slice(1)}
        </button>
      ))}
    </div>
  );
};

// Componente: Tarjeta de Métrica
interface MetricCardProps {
  metric: MetricItem;
}

export const MetricCard: React.FC<MetricCardProps> = ({ metric }) => {
  const getMetricClass = (type: string) => {
    switch (type) {
      case 'income':
        return { card: styles.metricCardIncome, icon: 'ri-arrow-down-line' };
      case 'expense':
        return { card: styles.metricCardExpense, icon: 'ri-arrow-up-line' };
      case 'saving':
        return { card: styles.metricCardSaving, icon: 'ri-safe-line' };
      default:
        return { card: '', icon: '' };
    }
  };

  const metricClass = getMetricClass(metric.type);

  return (
    <div className={styles.metricCard}>
      <div className={styles.metricHeader}>
        <h3 className={styles.metricTitle}>{metric.title}</h3>
        <div className={`${styles.metricIcon} ${metricClass.card}`}>
          <i className={metricClass.icon}></i>
        </div>
      </div>
      <p className={`${styles.metricAmount} ${metricClass.card}`}>{metric.amount}</p>
      <p className={styles.metricDate}>{metric.date}</p>
    </div>
  );
};

// Componente: Tarjeta de Insight
interface InsightCardProps {
  insight: InsightItem;
}

export const InsightCard: React.FC<InsightCardProps> = ({ insight }) => {
  return (
    <div className={`${styles.insightCard} ${styles[insight.gradientClass]}`}>
      <div className={styles.insightContent}>
        <div className={`${styles.insightIcon} ${styles[insight.iconBg]}`}>
          <i className={insight.icon}></i>
        </div>
        <div>
          <h3 className={styles.insightTitle}>{insight.title}</h3>
          <p className={styles.insightDescription}>{insight.description}</p>
        </div>
      </div>
    </div>
  );
};

// Componente: Resumen Financiero
interface FinancialSummaryProps {
  metrics: MetricItem[];
  insights: InsightItem[];
  periods?: string[];
  title?: string;
}

export const FinancialSummary: React.FC<FinancialSummaryProps> = ({
  metrics,
  insights,
  periods = ['mes', 'trimestre', 'año'],
  title = 'Resumen Financiero',
}) => {
  const [selectedPeriod, setSelectedPeriod] = useState(periods[0]);

  return (
    <div className={styles.summaryCard}>
      <div className={styles.summaryHeader}>
        <h2 className={styles.sectionTitle}>{title}</h2>
        <PeriodToggle
          periods={periods}
          selectedPeriod={selectedPeriod}
          onPeriodChange={setSelectedPeriod}
        />
      </div>

      <div className={styles.metricsGrid}>
        {metrics.map((metric, index) => (
          <MetricCard key={index} metric={metric} />
        ))}
      </div>

      <div className={styles.insightsGrid}>
        {insights.map((insight, index) => (
          <InsightCard key={index} insight={insight} />
        ))}
      </div>
    </div>
  );
};

// ==================== COMPONENTE PRINCIPAL ====================
const BalanceDashboard: React.FC = () => {
  const [showBalance, setShowBalance] = useState(true);

  const accounts: Account[] = [
    {
      id: 1,
      type: 'Cuenta Corriente',
      number: 'ES91 2100 0418 4502 0005 1332',
      balance: 5847.32,
      gradientClass: 'gradientTeal',
    },
    {
      id: 2,
      type: 'Cuenta de Ahorro',
      number: 'ES79 2100 0813 6101 2345 6789',
      balance: 12450.0,
      gradientClass: 'gradientGreen',
    },
    {
      id: 3,
      type: 'Cuenta Inversión',
      number: 'ES12 2100 0418 4502 0005 9876',
      balance: 8320.5,
      gradientClass: 'gradientGray',
    },
  ];

  const stats: StatItem[] = [
    { label: 'Ingresos del Mes', value: '€4,050.00' },
    { label: 'Gastos del Mes', value: '€1,505.50' },
    { label: 'Ahorro del Mes', value: '€2,544.50' },
  ];

  const metrics: MetricItem[] = [
    {
      title: 'Ingresos',
      amount: '+€4,050.00',
      date: '2025-01-15',
      type: 'income',
    },
    {
      title: 'Gastos',
      amount: '€1,505.50',
      date: '2025-01-15',
      type: 'expense',
    },
    {
      title: 'Ahorro',
      amount: '+€2,544.50',
      date: '2025-01-15',
      type: 'saving',
    },
  ];

  const insights: InsightItem[] = [
    {
      icon: 'ri-line-chart-line',
      title: 'Tendencia Positiva',
      description: 'Tus ahorros han crecido un 25.8%',
      gradientClass: 'gradientTealLight',
      iconBg: 'bgPrimary',
    },
    {
      icon: 'ri-trophy-line',
      title: 'Meta Alcanzada',
      description: 'Has superado tu objetivo mensual',
      gradientClass: 'gradientGreenLight',
      iconBg: 'bgSuccess',
    },
  ];

  const handleViewDetails = (accountId: number) => {
    console.log('Ver detalles de cuenta:', accountId);
  };

  return (
    <div className={styles.container}>
      <div className={styles.header}>
        <h1 className={styles.title}>Consulta de Saldo</h1>
        <p className={styles.subtitle}>Visualiza el estado de todas tus cuentas</p>
      </div>

      <TotalBalanceCard
        totalBalance="€26,617.82"
        stats={stats}
        showBalance={showBalance}
        onToggleBalance={() => setShowBalance(!showBalance)}
      />

      <AccountsGrid accounts={accounts} onViewDetails={handleViewDetails} />

      <FinancialSummary metrics={metrics} insights={insights} />
    </div>
  );
};

export default BalanceDashboard;