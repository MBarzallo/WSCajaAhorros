"use client";

import { useState } from "react";
import Link from "next/link";
import { useRouter } from "next/navigation";
import styles from "../../ui/stylesheets/Auth.module.css"
import { authService } from "../../services/auth.service";

// ========== COMPONENTE REGISTER ==========
export default function Register() {
  const router = useRouter();
  const [showPassword, setShowPassword] = useState(false);
  const [showConfirmPassword, setShowConfirmPassword] = useState(false);
  const [loading, setLoading] = useState(false);
  const [formData, setFormData] = useState({
    nombre: "",
    apellido: "",
    email: "",
    telefono: "",
    documento: "",
    password: "",
    confirmPassword: "",
    acceptTerms: false,
  });

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value, type, checked } = e.target;
    setFormData(prev => ({
      ...prev,
      [name]: type === "checkbox" ? checked : value
    }));
  };

  const handleSubmit = async () => {
    setLoading(true);
    try {
      const response = await authService.register(formData);
      console.log("Register success", response);
      router.push('/auth/login');
    } catch (error) {
      console.error("Register failed", error);
      alert("Error en el registro");
    } finally {
      setLoading(false);
    }
  };

  return (
    <>
      {/* Card */}
      <div className={styles.formContainer}>

        {/* Nombre y Apellido */}
        <div className={styles.formRow}>
          <div className={styles.formGroup}>
            <label className={styles.label}>Nombre</label>
            <input
              required
              className={styles.input}
              placeholder="Juan"
              type="text"
              value={formData.nombre}
              onChange={handleChange}
              name="nombre"
            />
          </div>
          <div className={styles.formGroup}>
            <label className={styles.label}>Apellido</label>
            <input
              required
              className={styles.input}
              placeholder="Pérez"
              type="text"
              value={formData.apellido}
              onChange={handleChange}
              name="apellido"
            />
          </div>
        </div>

        {/* Email */}
        <div className={styles.formGroup}>
          <label className={styles.label}>Correo Electrónico</label>
          <input
            required
            className={styles.input}
            placeholder="correo@ejemplo.com"
            type="email"
            value={formData.email}
            onChange={handleChange}
            name="email"
          />
        </div>

        {/* Teléfono */}
        <div className={styles.formGroup}>
          <label className={styles.label}>Teléfono</label>
          <input
            required
            className={styles.input}
            placeholder="+34 600 000 000"
            type="tel"
            value={formData.telefono}
            onChange={handleChange}
            name="telefono"
          />
        </div>

        {/* Documento */}
        <div className={styles.formGroup}>
          <label className={styles.label}>Documento de Identidad</label>
          <input
            required
            className={styles.input}
            placeholder="12345678A"
            type="text"
            value={formData.documento}
            onChange={handleChange}
            name="documento"
          />
        </div>

        {/* Password */}
        <div className={styles.formGroup}>
          <label className={styles.label}>Contraseña</label>
          <div className={styles.inputWrapper}>
            <input
              required
              className={`${styles.input} ${styles.inputWithButton}`}
              placeholder="••••••••"
              type={showPassword ? "text" : "password"}
              value={formData.password}
              onChange={handleChange}
              name="password"
            />
            <button
              type="button"
              className={styles.togglePassword}
              onClick={() => setShowPassword(!showPassword)}
            >
              <i className={showPassword ? "ri-eye-off-line" : "ri-eye-line"}></i>
            </button>
          </div>
        </div>

        {/* Confirm Password */}
        <div className={styles.formGroup}>
          <label className={styles.label}>Confirmar Contraseña</label>
          <div className={styles.inputWrapper}>
            <input
              required
              className={`${styles.input} ${styles.inputWithButton}`}
              placeholder="••••••••"
              type={showConfirmPassword ? "text" : "password"}
              value={formData.confirmPassword}
              onChange={handleChange}
              name="confirmPassword"
            />
            <button
              type="button"
              className={styles.togglePassword}
              onClick={() => setShowConfirmPassword(!showConfirmPassword)}
            >
              <i className={showConfirmPassword ? "ri-eye-off-line" : "ri-eye-line"}></i>
            </button>
          </div>
        </div>

        {/* Terms Checkbox */}
        <div className={styles.termsWrapper}>
          <input
            required
            className={styles.checkbox}
            type="checkbox"
            checked={formData.acceptTerms}
            onChange={handleChange}
            name="acceptTerms"
          />
          <label className={styles.termsLabel}>
            Acepto los{" "}
            <Link href="/terminos" className={styles.termsLink}>
              términos y condiciones
            </Link>{" "}
            y la{" "}
            <Link href="/privacidad" className={styles.termsLink}>
              política de privacidad
            </Link>
          </label>
        </div>

        {/* Submit Button */}
        <button
          type="button"
          className={styles.submitButton}
          onClick={handleSubmit}
          disabled={loading}
        >
          {loading ? "Registrando..." : "Crear Cuenta"}
        </button>
      </div>

      {/* Login Link */}
      <div className={styles.alternativeAction}>
        <p>
          ¿Ya tienes una cuenta?{" "}
          <Link href="/auth/login" className={styles.alternativeLink}>
            Iniciar Sesión
          </Link>
        </p>
      </div>

    </>
  );
}