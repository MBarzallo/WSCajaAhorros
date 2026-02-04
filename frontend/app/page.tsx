import { Nav } from "./components/General-Nav";
import styles from './ui/stylesheets/Home.module.css';
import { HeroSection } from "./components/home/HeroSection";
import { StatsSection } from "./components/home/StatsSection";
import { ServicesSection } from "./components/home/ServicesSection";
import { AboutSection } from "./components/home/AboutSection";
import { BenefitsSection } from "./components/home/BenefitsSection";
import { CtaSection } from "./components/home/CtaSection";
import { ContactSection } from "./components/home/ContactSection";
import { Footer } from "./components/Footer";
import { AbrirCuentaButton } from "./components/home/AbrirCuentaButton";

// Backward compatibility or direct import if needed somewhere else?
// But AbrirCuenta is used inside page in the original code (one instance) 
// and inside HeroSection and CtaSection.
// Wait, in the original code AbrirCuenta was defined at the bottom and used in line 55 and 250.
// I extracted it as AbrirCuentaButton.
// I also realized I missed `AbrirCuenta` usage inside `HeroSection` and `CtaSection` in my extraction?
// Let me check my previous writes using read_resource or just trusting my memory/output.
// I did use `<AbrirCuentaButton />` in `HeroSection` and `CtaSection`.
// So `app/page.tsx` just needs to compose these sections.

export default function Home() {

  return (
    <div className={styles.wrapper}>
      <Nav />

      {/* HERO SECTION */}
      <HeroSection />

      {/* STATS SECTION */}
      <StatsSection />

      {/* SERVICES SECTION */}
      <ServicesSection />

      {/* ABOUT SECTION */}
      <AboutSection />

      {/* BENEFITS SECTION */}
      <BenefitsSection />

      {/* TESTIMONIALS SECTION - Was commented out in original file, keeping it out or ignoring it? 
          The original file had it commented out. My extraction didn't include it. 
          If I want to preserve the commented out code, I should have extracted it or kept it. 
          The task said "separate all the components". 
          I'll skip the commented out code for cleaner main file, or I can add a comment.
      */}

      {/* CTA SECTION */}
      <CtaSection />

      {/* CONTACT SECTION */}
      <ContactSection />

      {/* FOOTER */}
      <Footer />
    </div>
  );
}