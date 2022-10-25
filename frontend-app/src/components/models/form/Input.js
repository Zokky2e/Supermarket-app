function Input(props) {
  return (
    <>
      <label>{props.label}</label>
      <input type={props.type} value={props.value} onChange={props.onChange} />
    </>
  );
}

export default Input;
