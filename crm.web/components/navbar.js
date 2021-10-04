import 'tailwindcss/tailwind.css'
import Link from 'next/link'

const Navbar = () =>{
    return(
        <nav>
            <div className="flex md:flex-row xs:flex-col w-100p md:h-20 xs:h-40 bg-sidebarBG content-center place-content-between px-5p text-white mb-2">
                <div className="text-left place-content-center fit-content h-auto my-auto md:text-2xl xs:text-md">
                    CRM
                </div>
                <div className="flex md:flex-row xs:flex-col md:space-x-10 md:space-y-0 xs:space-y-2 xs:space-x-0 h-auto my-auto md:text-2xl xs:text-md">
                    <Link href="/login">
                        <a className="text-left place-content-center fit-content h-auto cursor-pointer">Logowanie</a>
                    </Link>
                    <Link href="/register">
                        <a className="text-left place-content-center fit-content h-auto cursor-pointer">Dołącz do nas</a>
                    </Link>
                </div>
            </div>
        </nav>
        
    );
}

export default Navbar;