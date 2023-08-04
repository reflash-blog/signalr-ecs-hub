import React from "react";

type ButtonProps = {
    action: () => void;
    title: string;
}

function Button({ action, title }: ButtonProps) {
    return <button onClick={action}>{title}</button>;
}

export default Button;