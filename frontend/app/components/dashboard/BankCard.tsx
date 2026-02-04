"use client"
import { useState } from 'react';
import styles from '../../ui/stylesheets/Dashboard.module.css';

export function BankCard({ type = "", lastDigits = "", balance = "", variant = 'Primary' }) {
    const [showBalance, setShowBalance] = useState<boolean>(true);

    return (
        <div className={`${styles.bankCard} ${styles[`bankCard${variant}`]}`}>
            <div className={styles.bankCardHeader}>
                <div>
                    <p className={styles.bankCardType}>{type}</p>
                    <p className={styles.bankCardNumber}>**** {lastDigits}</p>
                </div>
                <i className="ri-bank-card-line" style={{ fontSize: '2rem', opacity: 0.8 }}></i>
            </div>
            <div className={styles.bankCardFooter}>
                <div>
                    <p className={styles.bankCardLabel}>
                        {type === 'Tarjeta de Débito' ? 'Saldo Disponible' : 'Disponible'}
                    </p>
                    <p className={styles.bankCardBalance}>
                        {showBalance ? balance : '€****.**'}
                    </p>
                </div>
                <button
                    onClick={() => setShowBalance(!showBalance)}
                    className={styles.toggleButton}
                    aria-label={showBalance ? 'Ocultar saldo' : 'Mostrar saldo'}
                >
                    <i className={`ri-eye-${showBalance ? 'off' : ''}-line`}></i>
                </button>
            </div>
        </div>
    );
};
