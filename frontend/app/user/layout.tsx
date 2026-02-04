import { UserNav } from "./user-nav";

export const metadata = {
  title: "User-Home",
};

export default function UserInterfaces({
  children,
}: {
  children: React.ReactNode;
}) {
   return (

    <div>
        <UserNav />
        {children}
    </div>
   )
}