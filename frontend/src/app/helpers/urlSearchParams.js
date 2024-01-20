export const getSearchParam = (search, paramName) => {
  const params = new URLSearchParams(search);
  return params.get(paramName);
};
