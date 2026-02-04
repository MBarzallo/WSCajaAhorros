// TransactionsHistory.tsx
'use client';

import React, { useState, useEffect } from 'react';
import styles from '../../ui/stylesheets/Historial.module.css';
import { userService } from '../../services/user.service';

// ==================== TIPOS ====================
interface SummaryCard {
  title: string;
  amount: string;
  icon: string;
  type: 'income' | 'expense' | 'balance';
}

interface Transaction {
  id: number;
  title: string;
  description: string;
  date: string;
  time: string;
  amount: number;
  type: 'income' | 'expense';
  status: 'completada' | 'pendiente' | 'rechazada';
  // Mapped from backend data which might use different fields
  transactionType?: string;
}

type FilterType = 'todas' | 'ingresos' | 'gastos';

// ==================== COMPONENTES REUTILIZABLES ====================

// Componente: Tarjeta de resumen (Total Ingresos, Gastos, Balance)
interface SummaryCardProps {
  card: SummaryCard;
}

export const SummaryCardComponent: React.FC<SummaryCardProps> = ({ card }) => {
  const getCardClass = () => {
    switch (card.type) {
      case 'income':
        return styles.summaryCardIncome;
      case 'expense':
        return styles.summaryCardExpense;
      case 'balance':
        return styles.summaryCardBalance;
      default:
        return '';
    }
  };

  return (
    <div className={styles.summaryCard}>
      <div className={styles.summaryHeader}>
        <h3 className={styles.summaryTitle}>{card.title}</h3>
        <div className={`${styles.summaryIcon} ${getCardClass()}`}>
          <i className={card.icon}></i>
        </div>
      </div>
      <p className={`${styles.summaryAmount} ${getCardClass()}`}>{card.amount}</p>
    </div>
  );
};

// Componente: Barra de búsqueda
interface SearchBarProps {
  value: string;
  onChange: (value: string) => void;
  placeholder?: string;
}

export const SearchBar: React.FC<SearchBarProps> = ({
  value,
  onChange,
  placeholder = 'Buscar transacciones...',
}) => {
  return (
    <div className={styles.searchContainer}>
      <i className="ri-search-line"></i>
      <input
        type="text"
        placeholder={placeholder}
        value={value}
        onChange={(e) => onChange(e.target.value)}
        className={styles.searchInput}
      />
    </div>
  );
};

// Componente: Filtro de tipo de transacción
interface FilterToggleProps {
  filters: FilterType[];
  selectedFilter: FilterType;
  onFilterChange: (filter: FilterType) => void;
}

export const FilterToggle: React.FC<FilterToggleProps> = ({
  filters,
  selectedFilter,
  onFilterChange,
}) => {
  const getFilterLabel = (filter: FilterType) => {
    const labels: Record<FilterType, string> = {
      todas: 'Todas',
      ingresos: 'Ingresos',
      gastos: 'Gastos',
    };
    return labels[filter];
  };

  return (
    <div className={styles.filterToggle}>
      {filters.map((filter) => (
        <button
          key={filter}
          className={`${styles.filterButton} ${selectedFilter === filter ? styles.filterButtonActive : ''
            }`}
          onClick={() => onFilterChange(filter)}
        >
          {getFilterLabel(filter)}
        </button>
      ))}
    </div>
  );
};

// Componente: Badge de estado
interface StatusBadgeProps {
  status: 'completada' | 'pendiente' | 'rechazada';
}

export const StatusBadge: React.FC<StatusBadgeProps> = ({ status }) => {
  const getStatusClass = () => {
    switch (status) {
      case 'completada':
        return styles.statusCompleted;
      case 'pendiente':
        return styles.statusPending;
      case 'rechazada':
        return styles.statusRejected;
      default:
        return '';
    }
  };

  return <span className={`${styles.statusBadge} ${getStatusClass()}`}>{status}</span>;
};

// Componente: Item de transacción individual
interface TransactionItemProps {
  transaction: Transaction;
  onClick?: () => void;
}

export const TransactionItem: React.FC<TransactionItemProps> = ({ transaction, onClick }) => {
  // Use transactionType from backend if available, matching local type 'income' | 'expense'
  const type = transaction.transactionType || transaction.type;
  const isIncome = type === 'income';
  const iconClass = isIncome ? styles.iconIncome : styles.iconExpense;
  const amountClass = isIncome ? styles.amountIncome : styles.amountExpense;
  const icon = isIncome ? 'ri-arrow-down-line' : 'ri-arrow-up-line';

  return (
    <div className={styles.transactionItem} onClick={onClick}>
      <div className={styles.transactionContent}>
        <div className={`${styles.transactionIcon} ${iconClass}`}>
          <i className={icon}></i>
        </div>
        <div className={styles.transactionInfo}>
          <p className={styles.transactionTitle}>{transaction.title}</p>
          <p className={styles.transactionDescription}>{transaction.description}</p>
          <p className={styles.transactionDateTime}>
            {transaction.date} • {transaction.time}
          </p>
        </div>
      </div>
      <div className={styles.transactionDetails}>
        <p className={`${styles.transactionAmount} ${amountClass}`}>
          {isIncome ? '+' : ''}€{Math.abs(transaction.amount).toFixed(2)}
        </p>
        <StatusBadge status={transaction.status} />
      </div>
    </div>
  );
};

// Componente: Lista de transacciones
interface TransactionListProps {
  transactions: Transaction[];
  onTransactionClick?: (transactionId: number) => void;
}

export const TransactionList: React.FC<TransactionListProps> = ({
  transactions,
  onTransactionClick,
}) => {
  if (transactions.length === 0) {
    return (
      <div className={styles.emptyState}>
        <i className="ri-inbox-line"></i>
        <p>No se encontraron transacciones</p>
      </div>
    );
  }

  return (
    <div className={styles.transactionList}>
      {transactions.map((transaction) => (
        <TransactionItem
          key={transaction.id}
          transaction={transaction}
          onClick={() => onTransactionClick?.(transaction.id)}
        />
      ))}
    </div>
  );
};

// Componente: Panel de transacciones (búsqueda + filtros + lista)
interface TransactionsPanelProps {
  transactions: Transaction[];
  onTransactionClick?: (transactionId: number) => void;
}

export const TransactionsPanel: React.FC<TransactionsPanelProps> = ({
  transactions,
  onTransactionClick,
}) => {
  const [searchQuery, setSearchQuery] = useState('');
  const [selectedFilter, setSelectedFilter] = useState<FilterType>('todas');

  const filteredTransactions = transactions.filter((transaction) => {
    // Filtro por búsqueda
    const matchesSearch =
      searchQuery === '' ||
      transaction.title.toLowerCase().includes(searchQuery.toLowerCase()) ||
      transaction.description.toLowerCase().includes(searchQuery.toLowerCase());

    // Filtro por tipo
    const type = transaction.transactionType || transaction.type;
    const matchesFilter =
      selectedFilter === 'todas' ||
      (selectedFilter === 'ingresos' && type === 'income') ||
      (selectedFilter === 'gastos' && type === 'expense');

    return matchesSearch && matchesFilter;
  });

  return (
    <div className={styles.transactionsPanel}>
      <div className={styles.panelControls}>
        <SearchBar value={searchQuery} onChange={setSearchQuery} />
        <FilterToggle
          filters={['todas', 'ingresos', 'gastos']}
          selectedFilter={selectedFilter}
          onFilterChange={setSelectedFilter}
        />
      </div>
      <TransactionList
        transactions={filteredTransactions}
        onTransactionClick={onTransactionClick}
      />
    </div>
  );
};

// Componente: Grid de tarjetas de resumen
interface SummaryGridProps {
  cards: SummaryCard[];
}

export const SummaryGrid: React.FC<SummaryGridProps> = ({ cards }) => {
  return (
    <div className={styles.summaryGrid}>
      {cards.map((card, index) => (
        <SummaryCardComponent key={index} card={card} />
      ))}
    </div>
  );
};

// ==================== COMPONENTE PRINCIPAL ====================
const TransactionsHistory: React.FC = () => {
  const summaryCards: SummaryCard[] = [
    {
      title: 'Total Ingresos',
      amount: '€4,945.00',
      icon: 'ri-arrow-down-line',
      type: 'income',
    },
    {
      title: 'Total Gastos',
      amount: '€790.79',
      icon: 'ri-arrow-up-line',
      type: 'expense',
    },
    {
      title: 'Balance',
      amount: '€4,154.21',
      icon: 'ri-wallet-line',
      type: 'balance',
    },
  ];

  const [transactions, setTransactions] = useState<Transaction[]>([]);

  useEffect(() => {
    userService.getTransactions().then(data => {
      // Ensure data matches Transaction interface
      setTransactions(data as Transaction[]);
    }).catch(console.error);
  }, [])

  const handleTransactionClick = (transactionId: number) => {
    console.log('Transacción seleccionada:', transactionId);
  };

  return (
    <div className={styles.container}>
      <div className={styles.header}>
        <h1 className={styles.title}>Historial de Transacciones</h1>
        <p className={styles.subtitle}>Consulta todas tus operaciones bancarias</p>
      </div>

      <SummaryGrid cards={summaryCards} />

      <TransactionsPanel
        transactions={transactions}
        onTransactionClick={handleTransactionClick}
      />
    </div>
  );
};

export default TransactionsHistory;