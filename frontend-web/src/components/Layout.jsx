import React from "react";
import { useParams, useSearchParams } from "react-router-dom";

function Layout() {
  const {id} = useParams();
  if(id === null || id === undefined){
    return <p>404 Not Found</p>
  }
  console.log(id);
  return <></>;
}

export default Layout;
