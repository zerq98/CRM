import axios from 'axios';
import ApiRestService from './ApiRestService';

export const logIn = async (props)=>{
    const httpService = new ApiRestService();
    process.env["NODE_TLS_REJECT_UNAUTHORIZED"] = 0;
    httpService.getInstance().post('api/Account/Login',
            JSON.stringify({
                    login: 'zerq',
                    password: 'Cromppidlak98#'
                }
            )
        ).then(function (response) {
            console.log(response);
        })
}