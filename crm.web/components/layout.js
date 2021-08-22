import Sidebar from "./sidebar";
import { useRouter } from 'next/router'
import { useEffect, useState } from 'react'
import 'tailwindcss/tailwind.css'
import cookieCutter from 'cookie-cutter'

export default function Layout({children}) {

    const [isLogged, SetIsLogged] = useState(false);
    const router = useRouter()
    useEffect(()=>{
        var userLogged = cookieCutter.get('tokenExpiration');
        if(userLogged===null){
            router.push('/login')
        }else {
          const expireDate = new Date(userLogged);
          const today= new Date();
            if(today>=expireDate){
                router.push('/login')
            }
            SetIsLogged(true);
        }
    })

    if(isLogged){
      return (
        <>
          <link href='https://unpkg.com/boxicons@2.0.9/css/boxicons.min.css' rel='stylesheet'></link>
          <div className="w-100p h-fullScreen">
            <Sidebar/>
            <div className="xs:w-80p xl:w-85p h-100p xs:ml-20p xl:ml-15p flex flex-col items-stretch">
              {children}
            </div>
          </div>
        </>
      )
    }else{
      return null
    }
  }