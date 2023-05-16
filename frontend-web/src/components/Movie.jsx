//TO DO....

import React from "react";
import { useParams} from "react-router-dom";


function Movie() {
    const {id} = useParams();
    if(id === null || id === undefined){
      return <p>404 Not Found</p>
    }  
    
    return <><p>Id {id}</p></>;
}

export default Movie;

