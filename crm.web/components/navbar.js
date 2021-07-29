import 'tailwindcss/tailwind.css'
import Link from 'next/link'

const Navbar = () =>{
    return(
        <nav>
            <div className="flex flex-row w-full h-20 bg-red content-center place-content-between px-10">
                <div className="text-left place-content-center fit-content h-auto my-auto text-2xl">
                    CRM
                </div>
                <div className="flex flex-row space-x-10 h-auto my-auto text-2xl">
                    <a className="text-left place-content-center fit-content h-auto cursor-pointer">Home</a>
                    <Link href="/login">
                        <a className="text-left place-content-center fit-content h-auto cursor-pointer">Logowanie</a>
                    </Link>
                </div>
            </div>
        </nav>
        
    );
}

export default Navbar;