import styles from '../../ui/stylesheets/Home.module.css';
import { AbrirCuentaButton } from './AbrirCuentaButton';

export function HeroSection() {
    return (
        <section className={`${styles.gradientPrimary} ${styles.heroSection}`}>
            <div className={styles.container}>
                <h1 className={styles.heroTitle}>
                    Sistema de gestión de Caja de ahorros
                </h1>
                <p className={styles.heroSubtitle}>
                    Gestiona tus finanzas de forma rápida, cómoda y segura.
                </p>

                <AbrirCuentaButton />

            </div>
        </section>
    );
}
