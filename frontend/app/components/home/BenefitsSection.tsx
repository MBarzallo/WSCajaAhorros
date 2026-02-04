import styles from '../../ui/stylesheets/Home.module.css';

export function BenefitsSection() {
    return (
        <section id="beneficios" className={styles.benefitsSection}>
            <div className={styles.container}>
                <div className={styles.sectionHeader}>
                    <h2 className={styles.sectionTitle}>¿Por Qué Nuestra Plataforma?</h2>
                    <p className={styles.sectionSubtitle}>
                        Optimizamos la administración de tu cooperativa con tecnología diseñada para el sector financiero.
                    </p>
                </div>
                <div className={styles.benefitsGrid}>
                    {[
                        {
                            icon: 'ri-speed-up-line',
                            title: 'Eficiencia Operativa',
                            desc: 'Automatiza procesos desde el registro de socios hasta los cierres diarios, eliminando el trabajo manual.'
                        },
                        {
                            icon: 'ri-shield-check-line',
                            title: 'Cumplimiento Legal',
                            desc: 'Genera libros auxiliares y reportes institucionales alineados con las normativas contables vigentes.'
                        },
                        {
                            icon: 'ri-database-2-line',
                            title: 'Integridad de Datos',
                            desc: 'Base de datos centralizada con trazabilidad completa de cada movimiento de ahorro y crédito.'
                        },
                        {
                            icon: 'ri-cloud-line',
                            title: 'Acceso Multiplataforma',
                            desc: 'Consulta estados de cuenta y gestiona la caja desde cualquier lugar con seguridad cifrada.'
                        },
                    ].map((item, idx) => (
                        <div key={idx} className={styles.benefitCard}>
                            <div className={`${styles.benefitIconContainer} ${styles.bgAccent}`}>
                                <i className={`${item.icon} ${styles.benefitIcon}`}></i>
                            </div>
                            <h3 className={styles.benefitTitle}>{item.title}</h3>
                            <p className={styles.benefitDesc}>{item.desc}</p>
                        </div>
                    ))}
                </div>
            </div>
        </section>
    );
}
