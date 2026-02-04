"use client";

import { useState } from "react";
import Link from "next/link";
import styles from '../ui/stylesheets/Nav.module.css'
import { Logo } from "./Logo";


export function Nav() {
  const [isOpen, setIsOpen] = useState(false);

  const menuItems = ["servicios", "nosotros", "beneficios", "contacto"];

  return (
    <nav className={styles.nav}>
      <div className={styles.container}>
        <div className={styles.navContent}>
          
          {/* Logo */}
          <Link href="/" className={styles.logo}>
            <Logo />
          </Link>

          {/* Menú Desktop */}
          <div className={styles.menuDesktop}>
            {menuItems.map((item) => (
              <a 
                key={item}
                href={`#${item}`} 
                className={styles.menuLink}
              >
                {item}
              </a>
            ))}
          </div>

          {/* Acciones Desktop */}
          <div className={styles.actionsDesktop}>
            <Link href="/auth/login" className={styles.loginLink}>
              Iniciar Sesión
            </Link>
            <Link href="/auth/register" className={styles.registerButton}>
              Abrir Cuenta
            </Link>
          </div>

          {/* Botón Hamburguesa Móvil */}
          <button 
            onClick={() => setIsOpen(!isOpen)}
            className={styles.hamburger}
          >
            <i className={isOpen ? "ri-close-line" : "ri-menu-line"}></i>
          </button>
        </div>
      </div>

      {/* Menú Móvil Desplegable */}
      <div className={`${styles.menuMobile} ${isOpen ? styles.menuMobileOpen : ''}`}>
        <div className={styles.menuMobileContent}>
          {menuItems.map((item) => (
            <a 
              key={item}
              href={`#${item}`} 
              onClick={() => setIsOpen(false)}
              className={styles.menuMobileLink}
            >
              {item}
            </a>
          ))}
          <div className={styles.menuMobileActions}>
            <Link 
              href="/auth/login" 
              onClick={() => setIsOpen(false)}
              className={styles.menuMobileLoginButton}
            >
              Iniciar Sesión
            </Link>
            <Link 
              href="/auth/register" 
              onClick={() => setIsOpen(false)}
              className={styles.menuMobileRegisterButton}
            >
              Abrir Cuenta
            </Link>
          </div>
        </div>
      </div>
    </nav>
  );
}