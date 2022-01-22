import 'tailwindcss/tailwind.css'
import Link from 'next/link'
import { signOut } from "next-auth/client"

const Sidebar = () => {

    return (
        <div className="xs:w-20p xl:w-15p h-screen bg-sidebarBG p-1 flex flex-col fixed">
            <div className='w-100p h-20 text-white xs: md:text-lg lg:text-2xl xl:text-4xl 4k:text-6xl font-bold flex flex-row md:space-x-5 xs:px-2 md:px-3 lg:px-4 xl:px-5 2xl:px-15p py-20p items-center'>
                <p>CRM</p>
            </div>
            <Link href='/dashboard'>
                <div className='w-100p h-10 text-white xs:text-xl md:text-lg lg:text-xl xl:text-2xl 4k:text-4xl font-bold flex flex-row items-center md:space-x-2 xs:px-4 md:px-2 xl:px-3 2xl:px-10p hover:bg-sidebarBGHover cursor-pointer'>
                    <i className='bx bxs-dashboard'  ></i>
                    <p className='xs:hidden md:block'>Dashboard</p>
                </div>
            </Link>
            <Link href='/todoList'>
                <div className='w-100p h-10 text-white xs:text-xl md:text-lg lg:text-xl xl:text-2xl 4k:text-4xl font-bold flex flex-row items-center md:space-x-2 xs:px-4 md:px-2 xl:px-3 2xl:px-10p hover:bg-sidebarBGHover cursor-pointer'>
                    <i className='bx bx-task'></i>
                    <p className='xs:hidden md:block'>Zadania</p>
                </div>
            </Link>
            <Link href='/leadList'>
                <div className='w-100p h-10 text-white xs:text-xl md:text-lg lg:text-xl xl:text-2xl 4k:text-4xl font-bold flex flex-row items-center md:space-x-2 xs:px-4 md:px-2 xl:px-3 2xl:px-10p hover:bg-sidebarBGHover cursor-pointer'>
                    <i className='bx bxs-user'></i>
                    <p className='xs:hidden md:block'>Leady</p>
                </div>
            </Link>
            <Link href='/opportunities'>
                <div className='w-100p h-10 text-white xs:text-xl md:text-lg lg:text-xl xl:text-2xl 4k:text-4xl font-bold flex flex-row items-center md:space-x-2 xs:px-4 md:px-2 xl:px-3 2xl:px-10p hover:bg-sidebarBGHover cursor-pointer'>
                    <i className='bx bx-target-lock'></i>
                    <p className='xs:hidden md:block'>Szanse</p>
                </div>
            </Link>
            <Link href='/productList'>
                <div className='w-100p h-10 text-white xs:text-xl md:text-lg lg:text-xl xl:text-2xl 4k:text-4xl font-bold flex flex-row items-center md:space-x-2 xs:px-4 md:px-2 xl:px-3 2xl:px-10p hover:bg-sidebarBGHover cursor-pointer'>
                    <i class='bx bxs-archive'></i>
                    <p className='xs:hidden md:block'>Produkty</p>
                </div>
            </Link>
            <Link href='/orders'>
                <div className='w-100p h-10 text-white xs:text-xl md:text-lg lg:text-xl xl:text-2xl 4k:text-4xl font-bold flex flex-row items-center md:space-x-2 xs:px-4 md:px-2 xl:px-3 2xl:px-10p hover:bg-sidebarBGHover cursor-pointer'>
                    <i className='bx bxs-cart'></i>
                    <p className='xs:hidden md:block'>Zamówienia</p>
                </div>
            </Link>
            <Link href='/administration'>
                <div className='w-100p h-10 text-white xs:text-xl md:text-lg lg:text-xl xl:text-2xl 4k:text-4xl font-bold flex flex-row items-center md:space-x-2 xs:px-4 md:px-2 xl:px-3 2xl:px-10p hover:bg-sidebarBGHover cursor-pointer'>
                    <i className='bx bxs-network-chart'></i>
                    <p className='xs:hidden md:block truncate'>Administracja</p>
                </div>
            </Link>
            <div className='w-100p h-10 text-white xs:text-xl md:text-lg lg:text-xl xl:text-2xl 4k:text-4xl font-bold flex flex-row items-center md:space-x-2 xs:px-4 md:px-2 xl:px-3 2xl:px-10p hover:bg-sidebarBGHover cursor-pointer'
            onClick={()=>signOut()}>
                <i class='bx bxs-exit'></i>
                <p className='xs:hidden md:block truncate'>Wyloguj</p>
            </div>
        </div>
    )
}

export default Sidebar;