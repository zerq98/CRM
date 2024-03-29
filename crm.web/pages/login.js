import 'tailwindcss/tailwind.css'
import Link from 'next/link'
import { useRouter } from 'next/router'
import { getProviders, signIn,getSession,providers } from 'next-auth/client'

export default function login({ providers}){
    const router = useRouter()

    // useEffect(()=>{
    //     var userLogged = cookieCutter.get('tokenExpiration')
    //     const today= new Date();
    //     if(userLogged!==null){
    //         const expireDate = new Date(userLogged);
    //         if(today<=expireDate){
    //             router.push('/dashboard')
    //         }
    //     }
    // })
    

    const handleLogin = function (event) {
        alert('Trwa logowanie, to może potrwać chwilę.')
        event.preventDefault();
        event.stopPropagation();

        signIn('credentials',
            {
                login:event.target.login.value,
                password:event.target.password.value,
                callbackUrl: '/dashboard'
            })
    }


    return (
        <div className="bg-loginBG h-screen bg-cover bg-no-repeat bg-center">
            <div className="bg-opacity-015 backdrop-filter backdrop-blur-lg bg-gray md:inset-x-30p top-10p xs:inset-x-20p
             md:w-35p xs:w-60p flex flex-col rounded-3xl relative bg-clip-padding flex flex-col justify-start items-center xs:py-5p md:py-2p">
                <div className="xs:text-sm md:text-xl lg:text-2xl font-bold">Bierzmy się do pracy</div>
                <div className="xs:text-xs md:text-md lg:text-lg">Zaloguj się do systemu</div>
                <div className="flex flex-col w-75p mx-10p h-50p">
                    <form className="flex flex-col xs:space-y-5 lg:space-y-10 items-center justify-center h-100p w-100p lg:mt-16" onSubmit={event => handleLogin(event)}>
                        <input id="login" name="login" type="text" placeholder="Nazwa użytkownika" required
                        className="shadow-inner w-100p xs:text-sm sm:text-lg md:text-xl xs:h-7 sm:h-8 md:h-9 text-black rounded-lg focus:outline-none xs:pl-2 sm:pl-3 lg:pl-5"/>
                        <input id="password" name="password" type="password" placeholder="Hasło" required
                        className="shadow-inner w-100p xs:text-sm sm:text-lg md:text-xl xs:h-7 sm:h-8 md:h-9 text-black rounded-lg focus:outline-none xs:pl-2 sm:pl-3 lg:pl-5"/>
                        <button className='bg-gray rounded-xl hover:bg-white hover:text-gray duration-300 xs:text-sm sm:text-lg md:text-xl font-bold text-white shadow p-2 md:w-70p xs:w-80p' 
                        type='submit'>
                            Zaloguj się
                        </button>
                    </form>
                <Link href="/register">
                    <div className="xs:text-xs md:text-md lg:text-lg cursor-pointer md:mt-5 text-blue 
                    transition duration-500 ease-in-out transform hover:-translate-y-1 hover:scale-125 mx-auto">Nie masz konta? Zarejestruj swoją firmę.</div>
                </Link>
                </div>
            </div>
        </div>
    )
}

login.getInitialProps = async(context)=>{
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
        providers: await providers(context)
    }
}