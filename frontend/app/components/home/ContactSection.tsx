import styles from '../../ui/stylesheets/Home.module.css';

export function ContactSection() {
    return (
        <section id="contacto" className={styles.contactSection}>
            <div className={styles.container}>
                <div className={styles.sectionHeader}>
                    <h2 className={styles.sectionTitle}>Contáctanos</h2>
                    <p className={styles.sectionSubtitle}>
                        Estamos aquí para ayudarte en todo lo que necesites
                    </p>
                </div>
                <div className={styles.contactGrid}>
                    <div className={styles.contactCard}>
                        <h3 className={styles.contactTitle}>Mateo Barzallo</h3>
                        <div className={`${styles.contactIcon} ${styles.bgAccent}`}>
                            <i className="ri-phone-line"></i>
                        </div>
                        <p>tel:+593 98 3935886</p>
                    </div>
                    <div className={styles.contactCard}>
                        <h3 className={styles.contactTitle}>Jorge Cueva</h3>
                        <div className={`${styles.contactIcon} ${styles.bgAccent}`}>
                            <i className="ri-phone-line"></i>
                        </div>
                        <p>tel:+593 96 263 7809</p>
                    </div>
                    <div className={styles.contactCard}>
                        <h3 className={styles.contactTitle}>Karen Quito</h3>
                        <div className={`${styles.contactIcon} ${styles.bgAccent}`}>
                            <i className="ri-phone-line"></i>
                        </div>
                        <p>tel:+593 96 274 1054</p>
                    </div>
                    <div className={styles.contactCard}>
                        <h3 className={styles.contactTitle}>Jennyfer Ramirez</h3>
                        <div className={`${styles.contactIcon} ${styles.bgAccent}`}>
                            <i className="ri-phone-line"></i>
                        </div>
                        <p>tel:+593 96 263 7809</p>
                    </div>

                </div>
            </div>  
        </section>
    );
}
