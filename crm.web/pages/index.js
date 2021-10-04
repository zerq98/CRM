import Image from 'next/image'
import Navbar from '../components/navbar'
import { getProviders, signIn,getSession,providers } from 'next-auth/client'

export default function Home({data}) {
  return (
    <div className="max-w-screen-xl h-screen mx-auto bg-white">
      <Navbar />
      <div className="w-100p h-80p flex flex-col overflow-auto overscroll-contain">
        <div className="w-100p h-70p bg-gray flex md:flex-row xs:flex-col items-center md:space-x-20 md:space-y-0 xs:space-x-0 xs:space-y-10 p-2">
          <div className="h-100p w-60p relative">
            <Image
              src="/Dashboard.png"
              alt="Zdjęcie pulpitu aplikacji"
              layout="fill"
              objectFit="fit"
            />
          </div>
          <div className="h-100p w-20p flex flex-col items-center justify-around text-white text-xl">
            <p className="text-2xl">Pulpit użytkownika</p>
            <p className="text-center">Pulpit użytkownika umożliwia przegląd statystyk pracy zalogowanego użytkownika oraz pozwala na szybki dostęp do listy zadań.</p>
          </div>
        </div>
      </div>
    </div>
  )
}


Home.getInitialProps = async(context)=>{
  const {req,res} = context;
  const session = await getSession({req});

  if(session && res && session.accessToken){
      res.writeHead(302,{
          Location: "/dashboard",
      })
      res.end()
      return;
  }

  return{
    session: undefined,
    data:null
}
}
