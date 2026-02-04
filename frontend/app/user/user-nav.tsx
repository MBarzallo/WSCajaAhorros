"use client";

import Link from "next/link";
import { useState, useRef, useEffect } from "react";
import styles from '../ui/stylesheets/User.module.css';
import { Logo } from "../components/Logo";
import { NavItem } from "../components/user/NavItem";

export function UserNav() {
  const [open, setOpen] = useState(false);
  const [profileOpen, setProfileOpen] = useState(false);
  const dropdownRef = useRef<HTMLDivElement>(null);

  const menuItems = [
    { href: "/user", label: "Panel", icon: "ri-dashboard-line" },
    { href: "/user/transferencias", label: "Transferencias", icon: "ri-exchange-line" },
    { href: "/user/saldo", label: "Saldo", icon: "ri-wallet-line" },
    { href: "/user/historial", label: "Historial", icon: "ri-history-line" },
  ];

  const profileItems = [
    // { href: "/user/profile", label: "Mi Perfil", icon: "ri-user-line" },
    // { href: "/configuracion", label: "Configuración", icon: "ri-settings-3-line" },
    { href: "/", label: "Cerrar Sesión", icon: "ri-logout-box-line", danger: true },
  ];

  // Cerrar dropdown al hacer clic fuera
  useEffect(() => {
    const handleClickOutside = (event: MouseEvent) => {
      if (dropdownRef.current && !dropdownRef.current.contains(event.target as Node)) {
        setProfileOpen(false);
      }
    };

    if (profileOpen) {
      document.addEventListener('mousedown', handleClickOutside);
    }

    return () => {
      document.removeEventListener('mousedown', handleClickOutside);
    };
  }, [profileOpen]);

  return (
    <>
      <nav className={styles.nav}>
        <div className={styles.container}>
          <div className={styles.content}>

            {/* Logo */}
            <Link href="/" className={styles.logo}>
              <Logo />
            </Link>

            {/* Menú Desktop */}
            <div className={styles.menuDesktop}>
              {menuItems.map(item => (
                <NavItem key={item.href} {...item} />
              ))}
            </div>

            {/* Acciones Desktop - Dropdown de Perfil */}
            <div className={styles.actions}>
              <div className={styles.profileDropdown} ref={dropdownRef}>
                <button
                  className={styles.user}
                  onClick={() => setProfileOpen(!profileOpen)}
                  aria-expanded={profileOpen}
                  aria-haspopup="true"
                >
                  <div className={styles.avatar}>JP</div>
                  <i className={`ri-arrow-down-s-line ${styles.arrowIcon} ${profileOpen ? styles.arrowOpen : ''}`}></i>
                </button>

                {/* Menú desplegable de perfil */}
                {profileOpen && (
                  <div className={styles.dropdownMenu}>
                    {profileItems.map(item => (
                      <Link
                        key={item.href}
                        href={item.href}
                        className={`${styles.dropdownItem} ${item.danger ? styles.dropdownItemDanger : ''}`}
                        onClick={() => setProfileOpen(false)}
                      >
                        <i className={item.icon}></i>
                        <span>{item.label}</span>
                      </Link>
                    ))}
                  </div>
                )}
              </div>
            </div>

            {/* Botón móvil */}
            <button
              onClick={() => setOpen(!open)}
              className={styles.mobileButton}
              aria-label="Menú"
            >
              <i className={open ? "ri-close-line" : "ri-menu-line"}></i>
            </button>
          </div>
        </div>
      </nav>

      {/* MENÚ MÓVIL DESPLEGABLE */}
      <div className={`${styles.mobileMenu} ${open ? styles.open : ""}`}>
        {/* Items de navegación */}
        {menuItems.map(item => (
          <Link
            key={item.href}
            href={item.href}
            className={styles.mobileItem}
            onClick={() => setOpen(false)}
          >
            <i className={item.icon}></i>
            {item.label}
          </Link>
        ))}

        {/* Separador */}
        <div className={styles.mobileSeparator}></div>

        {/* Items de perfil en móvil */}
        {profileItems.map(item => (
          <Link
            key={item.href}
            href={item.href}
            className={`${styles.mobileItem} ${item.danger ? styles.mobileItemDanger : ''}`}
            onClick={() => setOpen(false)}
          >
            <i className={item.icon}></i>
            {item.label}
          </Link>
        ))}
      </div>
    </>
  );
}