import styles from '../../ui/stylesheets/Home.module.css';

export function StatsSection() {
    return (
        <section className={styles.statsSection}>
            <div className={styles.container}>
                <div className={styles.statsGrid}>
                    {[
                        { icon: 'ri-user-line', val: '4', label: 'Clientes Activos' },
                        { icon: 'ri-money-euro-circle-line', val: '$150', label: 'Activos Gestionados' },
                        { icon: 'ri-trophy-line', val: '1', label: 'Años de Experiencia' },
                        { icon: 'ri-star-line', val: '50%', label: 'Satisfacción Cliente' },
                    ].map((stat, idx) => (
                        <div key={idx} className={styles.statItem}>
                            <div className={`${styles.statIconContainer} ${styles.bgAccent}`}>
                                <i className={`${stat.icon} ${styles.statIcon}`}></i>
                            </div>
                            <div className={styles.statValue}>{stat.val}</div>
                            <div className={styles.statLabel}>{stat.label}</div>
                        </div>
                    ))}
                </div>
            </div>
        </section>
    );
}
