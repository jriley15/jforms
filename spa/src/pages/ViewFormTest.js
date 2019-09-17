import React, { useEffect, useState } from "react";
import useRequest from "../hooks/useRequest";

export default function ViewFormTest({ match: { params } }) {
  const [snippets, setSnippets] = useState([]);
  const { get } = useRequest();

  useEffect(() => {
    async function getSnippets() {
      let response = await get("/Form/GetSnippets", { formId: params.formId });

      if (response.success) {
        setSnippets(response.data);
      }
    }
    getSnippets();
    return () => {};
  }, []);

  return (
    <>
      <div
        dangerouslySetInnerHTML={{
          __html: snippets.length > 0 ? snippets[0].code : "Loading"
        }}
      />
    </>
  );
}
