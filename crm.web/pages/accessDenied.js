import Layout from "../components/layout";
import React from 'react';
import { getSession } from "next-auth/client";
import Link from 'next/link'

function AccessDenied(data){
    return (
        <Layout>
            <div className="flex flex-col w-100p h-100p items-center bg-layoutBG p-7">
                <div className="w-100p h-100p bg-opacity-35 backdrop-filter backdrop-blur-lg bg-white rounded-lg flex flex-col items-center space-y-5 text-xl">
                    <p className="font-bold text-4xl">Przepraszamy!!</p>
                    <i class='bx bx-sad font-bold text-6xl'></i>
                    <p>Nie mogliśmy ukazać ci tej strony, ponieważ nie posiadasz odpowiednich uprawnień.</p>
                    <p>Skontaktuj się z administracją swojej firmy, aby uzyskać więcej informacji o wymaganych uprawnieniach.</p>
                    <Link href='dashboard'>
                        <p className="font-bold cursor-pointer text-blue text-2xl">Wróć na główną stronę</p>
                    </Link>
                </div>
            </div>
        </Layout>
    )
}

export async function getServerSideProps(context) {
    const sess = await getSession(context)
    if(sess){
        return { props: {}}
    }else{
        context.res.writeHead(302,{Location:"/login"})
        context.res.end();
    }
    return null
  }

export default AccessDenied;