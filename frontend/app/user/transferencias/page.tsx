"use client";
import { useState, useEffect } from 'react';
import styles from '../../ui/stylesheets/Transferencias.module.css'
import { userService } from '../../services/user.service';


export default function TransfersPage() {
  const [contacts, setContacts] = useState<any[]>([]);

  useEffect(() => {
    userService.getContacts().then(setContacts).catch(console.error);
  }, []);

  return (
    <div className={styles.wrapper} >
      <link
        href="https://cdn.jsdelivr.net/npm/remixicon@3.5.0/fonts/remixicon.css"
        rel="stylesheet"
      />
      {/* Header */}
      <div className={styles.header}>
        <h1 className={styles.title}>Transferencias</h1>
        <p className={styles.subtitle}>
          Envía dinero de forma rápida y segura
        </p>
      </div>

      <div className={styles.grid}>
        {/* FORMULARIO */}
        <div className={styles.formColumn}>
          <div className={styles.card}>
            <h2 className={styles.cardTitle}>Nueva Transferencia</h2>

            <form className={styles.form}>
              {/* Tipo */}
              <div>
                <label className={styles.label}>
                  Tipo de Transferencia
                </label>
                <div className={styles.typeGrid}>
                  <button type="button" className={styles.typeActive}>
                    Entre mis cuentas
                  </button>
                  <button type="button" className={styles.typeButton}>
                    A terceros
                  </button>
                </div>
              </div>

              {/* Cuenta origen */}
              <div>
                <label className={styles.label}>Cuenta de Origen</label>
                <select className={styles.select}>
                  <option value="">Selecciona una cuenta</option>
                  <option>
                    Cuenta Corriente - **** 4532 (€5,847.32)
                  </option>
                  <option>
                    Cuenta Ahorro - **** 6789 (€12,450.00)
                  </option>
                </select>
              </div>

              {/* Cuenta destino */}
              <div>
                <label className={styles.label}>Cuenta de Destino</label>
                <input
                  className={styles.input}
                  placeholder="ES00 0000 0000 0000 0000 0000"
                />
              </div>

              {/* Monto */}
              <div>
                <label className={styles.label}>Monto</label>
                <div className={styles.amount}>
                  <span>€</span>
                  <input
                    type="number"
                    step="0.01"
                    min="0.01"
                    placeholder="0.00"
                  />
                </div>
              </div>

              {/* Concepto */}
              <div>
                <label className={styles.label}>Concepto</label>
                <textarea
                  rows={3}
                  className={styles.textarea}
                  placeholder="Describe el motivo de la transferencia"
                />
              </div>

              <button type="submit" className={styles.submit}>
                Continuar
              </button>
            </form>
          </div>
        </div>

        {/* SIDEBAR */}
        <div className={styles.sideColumn}>
          {/* Contactos */}
          <div className={styles.card} style={{ display: 'none' }}>
            <h3 className={styles.cardSubtitle}>Contactos Frecuentes</h3>

            <div className={styles.contacts}>
              {contacts.map((c, i) => (
                <button key={i} className={styles.contact}>
                  <div className={styles.avatar}>{c.i || c.name.substring(0, 2)}</div>
                  <div>
                    <p className={styles.contactName}>{c.name}</p>
                    <p className={styles.contactIban}>{c.iban}</p>
                  </div>
                </button>
              ))}
            </div>
          </div>

          {/* Seguridad */}
          <div className={styles.security}>
            <div className={styles.securityHeader}>
              <i className="ri-shield-check-line"></i>
              <h3>Transferencias Seguras</h3>
            </div>
            <ul>
              <li><i className="ri-check-line" /> Encriptación de extremo a extremo</li>
              <li><i className="ri-check-line" /> Verificación en dos pasos</li>
              <li><i className="ri-check-line" /> Confirmación por SMS</li>
              <li><i className="ri-check-line" /> Sin comisiones entre cuentas</li>
            </ul>
          </div>
        </div>
      </div>
    </div>
  );
}
