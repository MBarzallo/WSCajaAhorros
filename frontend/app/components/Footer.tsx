import { Logo } from "./Logo";
import styles from "../ui/stylesheets/Home.module.css";

export function Footer() {
    return (
        <footer className={`${styles.gradientFooter} ${styles.footer}`}>
            <div className={styles.container}>
                <div className={styles.footerGrid}>
                    <Logo h={200} w={80} bg={true} />
                    <p className={styles.footerDesc}>
                        Tu banco digital de confianza. Innovación, seguridad y servicio excepcional.
                    </p>
                    {/* <div>
              <h4 className={styles.footerTitle}>Productos</h4>
              <ul className={styles.footerList}>
                {['Cuentas', 'Tarjetas', 'Inversiones', 'Seguros'].map(item => (
                  <li key={item}>
                    <a href="#" className={styles.footerLink}>{item}</a>
                  </li>
                ))}
              </ul>
            </div> */}
                    {/* <div>
              <h4 className={styles.footerTitle}>Empresa</h4>
              <ul className={styles.footerList}>
                {['Sobre Nosotros', 'Carreras', 'Contacto'].map(item => (
                  <li key={item}>
                    <a href="#" className={styles.footerLink}>{item}</a>
                  </li>
                ))}
              </ul>
            </div> */}
                    {/* <div>
              <h4 className={styles.footerTitle}>Legal</h4>
              <ul className={styles.footerList}>
                {['Privacidad', 'Términos', 'Cookies'].map(item => (
                  <li key={item}>
                    <a href="#" className={styles.footerLink}>{item}</a>
                  </li>
                ))}
              </ul>
            </div> */}
                </div>
                <div className={styles.footerBottom}>
                    <p className={styles.footerCopyright}>
                        © 2026 Shell Gestor de cuent de ahorros. Todos los derechos reservados.
                    </p>
                    {/* <div className={styles.socialLinks}>
              {['facebook-fill', 'twitter-x-line', 'instagram-line', 'linkedin-fill'].map(icon => (
                <a key={icon} href="#" className={styles.socialLink}>
                  <i className={`ri-${icon}`}></i>
                </a>
              ))}
            </div> */}
                </div>
            </div>
        </footer>
    );
}
