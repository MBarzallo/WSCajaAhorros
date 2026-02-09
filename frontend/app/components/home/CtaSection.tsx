import styles from '../../ui/stylesheets/Home.module.css';

export function CtaSection() {
    return (
        <section className={`${styles.gradientPrimary} ${styles.ctaSection}`}>
            <div className={styles.ctaContainer}>
                <h2 className={styles.ctaTitle}>Comienza Tu Viaje Financiero Hoy</h2>
                <p className={styles.ctaSubtitle}>
                    Abre tu cuenta en menos de 5 minutos y disfruta de todos nuestros servicios sin comisiones
                </p>
            </div>
        </section>
    );
}
