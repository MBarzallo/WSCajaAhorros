import styles from '../../ui/stylesheets/Home.module.css';

interface ServiceCardProps {
    icon?: string;
    title?: string;
    desc?: string;
    features?: string[];
}

export const ServiceCard = ({
    icon,
    title,
    desc,
    features = [],
}: ServiceCardProps) => {
    return (
        <div className={styles.serviceCard}>
            <div className={`${styles.gradientLight} ${styles.iconContainer}`}>
                <i className={`${icon} ${styles.serviceIcon} ${styles.textAccent}`}></i>
            </div>
            <h3 className={styles.serviceTitle}>{title}</h3>
            <p className={styles.serviceDesc}>{desc}</p>
            <ul className={styles.featureList}>
                {features.map((f, i) => (
                    <li key={i} className={styles.featureItem}>
                        <i className={`ri-check-line ${styles.checkIcon} ${styles.textAccent}`}></i>
                        {f}
                    </li>
                ))}
            </ul>
        </div>
    );
};
