// components/PowerBIEmbed.tsx
const PowerBIEmbed = () => {
  return (
    <div className="flex justify-center items-center w-full h-screen">
      <iframe
        title="DATN"
        src="https://app.powerbi.com/reportEmbed?reportId=953347c5-62c1-4794-9636-22ee691d9f69&autoAuth=true&ctid=447080b4-b9c6-4b0b-92fd-b543a68b4e97"
        allowFullScreen={true}
        className="w-full h-full border-0"
      ></iframe>
    </div>
  );
};

export default PowerBIEmbed;
