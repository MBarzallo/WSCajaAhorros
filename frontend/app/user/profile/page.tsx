// UserProfile.tsx
'use client';

import React, { useState } from 'react';
import styles from '../../ui/stylesheets/Perfil.module.css';

// ==================== TIPOS ====================
interface UserData {
  nombre: string;
  email: string;
  telefono: string;
  documento: string;
  fechaNacimiento: string;
  direccion: string;
  iniciales: string;
}

interface FormField {
  label: string;
  name: keyof UserData;
  type: 'text' | 'email' | 'tel' | 'date';
  value: string;
}

interface StatCard {
  label: string;
  value: string;
  icon: string;
  gradientClass: 'gradientTeal' | 'gradientBlue' | 'gradientAmber';
}

// ==================== COMPONENTES REUTILIZABLES ====================

// Componente: Avatar con iniciales
interface AvatarProps {
  initials: string;
  size?: 'small' | 'medium' | 'large';
  editable?: boolean;
}

export const Avatar: React.FC<AvatarProps> = ({ initials, size = 'large', editable = false }) => {
  const sizeClass = size === 'small' ? styles.avatarSmall : size === 'medium' ? styles.avatarMedium : styles.avatarLarge;
  
  return (
    <div className={styles.avatarWrapper}>
      <div className={`${styles.avatar} ${sizeClass}`}>
        <div className={styles.avatarContent}>
          {initials}
        </div>
      </div>
      {editable && (
        <button className={styles.avatarEditButton}>
          <i className="ri-camera-line"></i>
        </button>
      )}
    </div>
  );
};

// Componente: Header con banner y avatar
interface ProfileHeaderProps {
  user: UserData;
  onEditClick?: () => void;
  isEditing?: boolean;
}

export const ProfileHeader: React.FC<ProfileHeaderProps> = ({ user, onEditClick, isEditing = false }) => {
  return (
    <div className={styles.headerContainer}>
      <div className={styles.banner}></div>
      <div className={styles.avatarSection}>
        <Avatar initials={user.iniciales} editable={isEditing} />
      </div>
      <div className={styles.headerInfo}>
        <div className={styles.userInfo}>
          <h1 className={styles.userName}>{user.nombre}</h1>
          <p className={styles.userEmail}>{user.email}</p>
        </div>
        <button className={styles.editButton} onClick={onEditClick}>
          <i className={isEditing ? 'ri-close-line' : 'ri-edit-line'}></i>
          {isEditing ? 'Cancelar' : 'Editar Perfil'}
        </button>
      </div>
    </div>
  );
};

// Componente: Input de formulario
interface FormInputProps {
  label: string;
  type: 'text' | 'email' | 'tel' | 'date';
  name: string;
  value: string;
  disabled?: boolean;
  onChange?: (name: string, value: string) => void;
}

export const FormInput: React.FC<FormInputProps> = ({
  label,
  type,
  name,
  value,
  disabled = false,
  onChange,
}) => {
  return (
    <div className={styles.formGroup}>
      <label className={styles.label}>{label}</label>
      <input
        type={type}
        name={name}
        value={value}
        disabled={disabled}
        onChange={(e) => onChange?.(name, e.target.value)}
        className={styles.input}
      />
    </div>
  );
};

// Componente: Formulario de usuario
interface UserFormProps {
  fields: FormField[];
  disabled?: boolean;
  onChange?: (name: string, value: string) => void;
}

export const UserForm: React.FC<UserFormProps> = ({ fields, disabled = false, onChange }) => {
  return (
    <form className={styles.form}>
      <div className={styles.formGrid}>
        {fields.map((field) => (
          <FormInput
            key={field.name}
            label={field.label}
            type={field.type}
            name={field.name}
            value={field.value}
            disabled={disabled}
            onChange={onChange}
          />
        ))}
      </div>
    </form>
  );
};

// Componente: Tarjeta de estadística
interface StatCardProps {
  stat: StatCard;
}

export const StatCardComponent: React.FC<StatCardProps> = ({ stat }) => {
  return (
    <div className={`${styles.statCard} ${styles[stat.gradientClass]}`}>
      <div className={styles.statContent}>
        <div>
          <p className={styles.statLabel}>{stat.label}</p>
          <p className={styles.statValue}>{stat.value}</p>
        </div>
        <div className={styles.statIcon}>
          <i className={stat.icon}></i>
        </div>
      </div>
    </div>
  );
};

// Componente: Sección de estadísticas
interface StatsSection {
  title?: string;
  stats: StatCard[];
}

export const StatsSection: React.FC<StatsSection> = ({ title = 'Estadísticas de Cuenta', stats }) => {
  return (
    <div className={styles.statsSection}>
      <h2 className={styles.statsTitle}>{title}</h2>
      <div className={styles.statsGrid}>
        {stats.map((stat, index) => (
          <StatCardComponent key={index} stat={stat} />
        ))}
      </div>
    </div>
  );
};

// Componente: Botón de acción
interface ActionButtonProps {
  children: React.ReactNode;
  onClick?: () => void;
  variant?: 'primary' | 'secondary' | 'danger';
  icon?: string;
  fullWidth?: boolean;
}

export const ActionButton: React.FC<ActionButtonProps> = ({
  children,
  onClick,
  variant = 'primary',
  icon,
  fullWidth = false,
}) => {
  const variantClass = variant === 'secondary' ? styles.buttonSecondary : variant === 'danger' ? styles.buttonDanger : styles.buttonPrimary;
  const widthClass = fullWidth ? styles.buttonFullWidth : '';

  return (
    <button className={`${styles.actionButton} ${variantClass} ${widthClass}`} onClick={onClick}>
      {icon && <i className={icon}></i>}
      {children}
    </button>
  );
};

// Componente: Tarjeta de perfil completa
interface ProfileCardProps {
  user: UserData;
  fields: FormField[];
  stats: StatCard[];
  onSave?: (updatedData: Partial<UserData>) => void;
}

export const ProfileCard: React.FC<ProfileCardProps> = ({ user, fields, stats, onSave }) => {
  const [isEditing, setIsEditing] = useState(false);
  const [formData, setFormData] = useState<UserData>(user);

  const handleEditToggle = () => {
    if (isEditing) {
      // Cancelar: restaurar datos originales
      setFormData(user);
    }
    setIsEditing(!isEditing);
  };

  const handleFieldChange = (name: string, value: string) => {
    setFormData((prev) => ({
      ...prev,
      [name]: value,
    }));
  };

  const handleSave = () => {
    onSave?.(formData);
    setIsEditing(false);
  };

  const updatedFields = fields.map((field) => ({
    ...field,
    value: formData[field.name],
  }));

  return (
    <div className={styles.profileCard}>
      <ProfileHeader user={formData} onEditClick={handleEditToggle} isEditing={isEditing} />

      <div className={styles.cardContent}>
        <UserForm fields={updatedFields} disabled={!isEditing} onChange={handleFieldChange} />

        {isEditing && (
          <div className={styles.formActions}>
            <ActionButton variant="secondary" onClick={handleEditToggle}>
              Cancelar
            </ActionButton>
            <ActionButton variant="primary" icon="ri-save-line" onClick={handleSave}>
              Guardar Cambios
            </ActionButton>
          </div>
        )}

        <StatsSection stats={stats} />
      </div>
    </div>
  );
};

// ==================== COMPONENTE PRINCIPAL ====================
const UserProfile: React.FC = () => {
  const userData: UserData = {
    nombre: 'Juan Pérez',
    email: 'juan.perez@email.com',
    telefono: '+34 612 345 678',
    documento: '12345678A',
    fechaNacimiento: '1990-05-15',
    direccion: 'Calle Principal 123, Madrid',
    iniciales: 'JP',
  };

  const formFields: FormField[] = [
    { label: 'Nombre Completo', name: 'nombre', type: 'text', value: userData.nombre },
    { label: 'Correo Electrónico', name: 'email', type: 'email', value: userData.email },
    { label: 'Teléfono', name: 'telefono', type: 'tel', value: userData.telefono },
    { label: 'Documento de Identidad', name: 'documento', type: 'text', value: userData.documento },
    { label: 'Fecha de Nacimiento', name: 'fechaNacimiento', type: 'date', value: userData.fechaNacimiento },
    { label: 'Dirección', name: 'direccion', type: 'text', value: userData.direccion },
  ];

  const stats: StatCard[] = [
    {
      label: 'Transferencias Realizadas',
      value: '127',
      icon: 'ri-exchange-line',
      gradientClass: 'gradientTeal',
    },
    {
      label: 'Cuenta Activa Desde',
      value: '2 años',
      icon: 'ri-calendar-line',
      gradientClass: 'gradientBlue',
    },
    {
      label: 'Nivel de Verificación',
      value: 'Premium',
      icon: 'ri-shield-check-line',
      gradientClass: 'gradientAmber',
    },
  ];

  const handleSave = (updatedData: Partial<UserData>) => {
    console.log('Datos actualizados:', updatedData);
    // Aquí iría la lógica para guardar en el backend
  };

  return (
    <div className={styles.container}>
      <ProfileCard user={userData} fields={formFields} stats={stats} onSave={handleSave} />
    </div>
  );
};

export default UserProfile;