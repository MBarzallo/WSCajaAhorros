import styles from '../../ui/stylesheets/Dashboard.module.css';

export function StatCard({ title = "", amount = "", percentage = "", trend = "", icon = "", variant = 'success' }) {
    return (
        <div className={styles.statCard}>
            <div className={styles.statCardHeader}>
                <h3 className={styles.statCardTitle}>{title}</h3>
                <div className={`${styles.statCardIconWrapper} ${styles[`statCardIcon${variant}`]}`}>
                    <i className={icon}></i>
                </div>
            </div>
            <p className={styles.statCardAmount}>{amount}</p>
            <p className={`${styles.statCardPercentage} ${styles[`statCardPercentage${variant}-`]}`}>
                <i className={`ri-arrow-${trend}-line`}></i>
                {percentage} este mes
            </p>
        </div>
    );
};
