import Head from 'next/head'
import Navbar from '../components/navbar'
import { useRouter } from 'next/router'
import { getProviders, signIn,getSession,providers } from 'next-auth/client'

export default function Home({data}) {
  return (
    <div className="max-w-screen-xl mx-auto bg-white">
      <Navbar />
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
