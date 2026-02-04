import Link from "next/link";
import styles from '../../ui/stylesheets/Home.module.css';

export function AbrirCuentaButton() {
    return (
        <Link href="/auth/register">
            <button className={`${styles.heroCta} ${styles.textAccent}`}>
                Abrir Cuenta Gratis
            </button>
        </Link>
    )
}
