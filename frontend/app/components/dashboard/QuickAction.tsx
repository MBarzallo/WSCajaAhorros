import styles from '../../ui/stylesheets/Dashboard.module.css';

export function QuickAction({ icon = "", label = "", variant = 'Primary', href = "" }) {
    return (
        <a
            href={href}
            className={`${styles.quickAction} ${styles[`quickAction${variant}`]}`}
        >
            <div className={`${styles.quickActionIcon} ${styles[`quickActionIcon${variant}`]}`}>
                <i className={icon}></i>
            </div>
            <span className={styles.quickActionLabel}>{label}</span>
        </a>
    );
};
