import styles from '../ui/stylesheets/Auth.module.css'
import Link from 'next/link'
import { Logo } from '../components/Logo'

export const metadata = {
  title: "Auth",
};


export default function AuthLayout(
    { children }: { children: React.ReactNode }
){
    return (
      <div className={styles.container}>
      <div className={styles.wrapper}>
        
        {/* Header */}
        <div className={styles.header}>
          
          <Link href="/" className="flex items-center justify-center">
            <Logo h={200} w={150}/>
          </Link>
          <h2 className={styles.title} >Shell</h2>
          <h3 className={styles.subtitle} >Bienvenido</h3>
          <p className={styles.label} >Sistema de gestión de caja de ahorros</p>

        </div>

        {/* Card */}
        <div className={styles.card}>
          {children}
      </div>
    </div>
    </div>
        
    )
}