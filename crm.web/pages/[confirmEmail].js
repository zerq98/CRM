import Link from "next/link";
import react, { useEffect } from "react";
import { useRouter } from 'next/router'
import {server} from '../server'


export default function EmailConfirmation (){

    const router = useRouter()
    

    async function confirm(userId,token){
        if (process.env.NODE_TLS_REJECT_UNAUTHORIZED !== "0") {
            process.env.NODE_TLS_REJECT_UNAUTHORIZED = "0";
        }
        if(userId!==null && token!==null && userId!=='' && token!==''){
            const res = await fetch(server+"Account/ConfirmEmail?userId="+userId+"&token="+token, {
                method: 'POST',
                headers: {
                    accept: '*/*',
                    "Content-Type": "application/json"
                }
            })
    
            const resData = await res.json()
    
            if (resData.code === 200) {
                alert('Udało się potwierdzić adres email.')
            } else {
                alert(resData.errorMessage)
            }
        }
    }

    useEffect(() => {
        const userId=router.query.userId;
        const token =router.query.token;
        confirm(userId,token)
      }, [])

      return (
        <div className="bg-loginBG h-screen bg-cover bg-no-repeat bg-center">
            <div className="bg-opacity-015 backdrop-filter backdrop-blur-lg bg-gray md:inset-x-15p xl:inset-x-20p xs:top-20p md:top-15p 2xl:top-10p xs:inset-x-10p
            md:w-70p xl:w-60p xs:w-80p flex flex-col rounded-3xl relative bg-clip-padding flex flex-col justify-start items-center xs:py-2p md:py-1p text-center space-y-5p xs:px-4">
                <div className="xs:text-xs md:text-xl lg:text-2xl font-bold">Brawo udało ci się potwierdzić swój adres!</div>
                <div className="xs:text-xs md:text-xl lg:text-2xl font-bold">Adres email podany podczas rejestracji został potwierdzony. Możesz teraz spokojnie korzystać z systemu.</div>
                <div className="xs:text-xs md:text-xl lg:text-2xl font-bold">Aby zacząć działanie z naszym systemem, 
                    <Link href='/login'>
                        <span className="md:hover:text-blue xs:text-blue md:text-black cursor-pointer"> kliknij tutaj.</span>
                    </Link>
                </div>
            </div>
        </div>
    )
}