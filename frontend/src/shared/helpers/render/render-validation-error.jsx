export const renderValidationErrorsObject = (errors) => {
  if (errors) {
    return Object.values(errors).map((error, index) => (
      <div key={index}>{error}</div>
    ));
  }

  return <div>Failed outside of validation!</div>;
};
