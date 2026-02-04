import styles from '../../ui/stylesheets/Home.module.css';
import { ServiceCard } from './ServiceCard';

export function ServicesSection() {
    return (
        <section id="servicios" className={styles.servicesSection}>
            <div className={styles.container}>
                <div className={styles.sectionHeader}>
                    <h2 className={styles.sectionTitle}>Nuestros Servicios</h2>
                    <p className={styles.sectionSubtitle}>
                        Soluciones financieras diseñadas a tus necesidades
                    </p>
                </div>
                <div className={styles.servicesGrid}>
                    <ServiceCard
                        icon="ri-admin-line"
                        title="Control de Accesos"
                        desc="Administración robusta de usuarios para garantizar la integridad de la información."
                        features={["Gestión de usuarios", "Perfiles de acceso", "Seguridad por roles"]}
                    />
                    <ServiceCard
                        icon="ri-group-line"
                        title="Gestión de Socios"
                        desc="Control detallado de los miembros de la institución y sus cuentas asociadas."
                        features={["Registro de socios", "Cuentas de ahorro", "Expediente digital"]}
                    />
                    <ServiceCard
                        icon="ri-exchange-funds-line"
                        title="Operaciones Financieras"
                        desc="Registro integral de movimientos de flujo de caja y transacciones monetarias."
                        features={["Depósitos y retiros", "Aportaciones", "Ingresos y egresos"]}
                    />
                    <ServiceCard
                        icon="ri-hand-coin-line"
                        title="Módulo de Créditos"
                        desc="Ciclo de vida completo del préstamo, desde la solicitud hasta la recuperación."
                        features={["Evaluación y aprobación", "Desembolso", "Control de pagos"]}
                    />
                    <ServiceCard
                        icon="ri-settings-4-line"
                        title="Automatización Contable"
                        desc="Integración fluida con sistemas contables externos mediante asientos automáticos."
                        features={["Asientos automáticos", "Enlace contable", "Reducción de errores"]}
                    />
                    <ServiceCard
                        icon="ri-file-list-3-line"
                        title="Reportería Avanzada"
                        desc="Visualización clara del estado institucional a través de documentos oficiales."
                        features={["Libro Diario", "Cartera de Créditos", "Resumen de Aportaciones"]}
                    />
                    <ServiceCard
                        icon="ri-terminal-box-line"
                        title="Servicio Web (API)"
                        desc="Consultas en tiempo real para aplicaciones externas o portales de usuario."
                        features={["Últimos 3 movimientos", "Seguridad REST", "Consulta instantánea"]}
                    />
                </div>
            </div>
        </section>
    );
}
