import Link from "next/link";
import styles from '../../ui/stylesheets/User.module.css';

interface NavItemProps {
    href: string;
    label: string;
    icon: string;
}

export function NavItem({
    href,
    label,
    icon,
}: NavItemProps) {
    return (
        <Link href={href} className={styles.navItem}>
            <i className={icon}></i>
            {label}
        </Link>
    );
}
