interface TagFiledProps {
  cssClass: string;
  context: string;
}

const TagFiled: React.FC<TagFiledProps> = ({ cssClass, context }) => {
  return (
    <span
      className={`${cssClass} opacity-75 px-2 py-1 rounded whitespace-nowrap overflow-hidden text-ellipsis`}
    >
      {context}
    </span>
  );
};

export default TagFiled;
