import "./Button.css";

function Button(props) {
  return (
    <button style={{ padding: props.padding }} onClick={props.onClick}>
      {props.text}
    </button>
  );
}

export default Button;
