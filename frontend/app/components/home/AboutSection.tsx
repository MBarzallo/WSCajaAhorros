import Image from "next/image";
import styles from '../../ui/stylesheets/Home.module.css';

export function AboutSection() {
    return (
        <section id="nosotros" className={`${styles.aboutSection} ${styles.gradientLight}`}>
            <div className={styles.container}>
                <div className={styles.aboutGrid}>
                    <div className={styles.aboutContent}>
                        <h2 className={styles.sectionTitle}>Sobre Nosotros</h2>
                        <p className={styles.aboutText}>
                            Estudiantes de la universidad Politécnica salesiana encargados en el desarrollo de software.
                        </p>
                    </div>
                    <div className={styles.aboutImageContainer}>
                        <Image
                            alt="Equipo"
                            width={600}
                            height={700}
                            className={styles.aboutImage}
                            src="/home-pic.jpg"
                        />
                    </div>
                </div>
            </div>
        </section>
    );
}
