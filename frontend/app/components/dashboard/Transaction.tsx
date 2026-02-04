import styles from '../../ui/stylesheets/Dashboard.module.css';

interface TransactionProps {
    type: string;
    description?: string;
    sender: string;
    date: string;
    amount: string;
    isPositive: boolean;
}

export const Transaction: React.FC<TransactionProps> = ({
    type,
    sender,
    date,
    amount,
    isPositive
}) => {
    return (
        <div className={styles.transaction}>
            <div className={styles.transactionContent}>
                <div className={`${styles.transactionIcon} ${isPositive ? styles.transactionIconPositive : styles.transactionIconNegative}`}>
                    <i className={`ri-arrow-${isPositive ? 'down' : 'up'}-line`}></i>
                </div>
                <div className={styles.transactionDetails}>
                    <p className={styles.transactionType}>{type}</p>
                    <p className={styles.transactionSender}>{sender}</p>
                    <p className={styles.transactionDate}>{date}</p>
                </div>
            </div>
            <div className={styles.transactionAmount}>
                <p className={isPositive ? styles.amountPositive : styles.amountNegative}>
                    {isPositive ? '+' : ''}{amount}
                </p>
            </div>
        </div>
    );
};
