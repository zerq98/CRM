import Link from "next/link";
import react, { useEffect } from "react";

export default function Registered (){
    return (
        <div className="bg-loginBG h-screen bg-cover bg-no-repeat bg-center">
            <div className="bg-opacity-015 backdrop-filter backdrop-blur-lg bg-gray md:inset-x-15p xl:inset-x-20p xs:top-20p md:top-15p 2xl:top-10p xs:inset-x-10p
            md:w-70p xl:w-60p xs:w-80p flex flex-col rounded-3xl relative bg-clip-padding flex flex-col justify-start items-center xs:py-2p md:py-1p text-center space-y-5p xs:px-4">
                <div className="xs:text-xs md:text-xl lg:text-2xl font-bold">Brawo udało ci się zarejestrować firmę!</div>
                <div className="xs:text-xs md:text-xl lg:text-2xl font-bold">Na podany adres email został wysłany link, aby móc potwierdzić email.</div>
                <div className="xs:text-xs md:text-xl lg:text-2xl font-bold">Aby zacząć działanie z naszym systemem, 
                    <Link href='/login'>
                        <span className="md:hover:text-blue xs:text-blue md:text-black cursor-pointer"> kliknij tutaj.</span>
                    </Link>
                </div>
            </div>
        </div>
    )
}