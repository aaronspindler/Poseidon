namespace Poseidon.Models
{
    public class AssetPairs
    {
        public object[] error { get; set; }
        public Result result { get; set; }

        public class Result
        {
            public DASHEUR DASHEUR { get; set; }
            public DASHUSD DASHUSD { get; set; }
            public DASHXBT DASHXBT { get; set; }
            public EOSETH EOSETH { get; set; }
            public EOSEUR EOSEUR { get; set; }
            public EOSUSD EOSUSD { get; set; }
            public EOSXBT EOSXBT { get; set; }
            public GNOETH GNOETH { get; set; }
            public GNOEUR GNOEUR { get; set; }
            public GNOUSD GNOUSD { get; set; }
            public GNOXBT GNOXBT { get; set; }
            public USDTZUSD USDTZUSD { get; set; }
            public XETCXETH XETCXETH { get; set; }
            public XETCXXBT XETCXXBT { get; set; }
            public XETCZEUR XETCZEUR { get; set; }
            public XETCZUSD XETCZUSD { get; set; }
            public XETHXXBT XETHXXBT { get; set; }
            public XETHXXBTD XETHXXBTd { get; set; }
            public XETHZCAD XETHZCAD { get; set; }
            public XETHZCADD XETHZCADd { get; set; }
            public XETHZEUR XETHZEUR { get; set; }
            public XETHZEURD XETHZEURd { get; set; }
            public XETHZGBP XETHZGBP { get; set; }
            public XETHZGBPD XETHZGBPd { get; set; }
            public XETHZJPY XETHZJPY { get; set; }
            public XETHZJPYD XETHZJPYd { get; set; }
            public XETHZUSD XETHZUSD { get; set; }
            public XETHZUSDD XETHZUSDd { get; set; }
            public XICNXETH XICNXETH { get; set; }
            public XICNXXBT XICNXXBT { get; set; }
            public XLTCXXBT XLTCXXBT { get; set; }
            public XLTCZEUR XLTCZEUR { get; set; }
            public XLTCZUSD XLTCZUSD { get; set; }
            public XMLNXETH XMLNXETH { get; set; }
            public XMLNXXBT XMLNXXBT { get; set; }
            public XREPXETH XREPXETH { get; set; }
            public XREPXXBT XREPXXBT { get; set; }
            public XREPZEUR XREPZEUR { get; set; }
            public XREPZUSD XREPZUSD { get; set; }
            public XXBTZCAD XXBTZCAD { get; set; }
            public XXBTZCADD XXBTZCADd { get; set; }
            public XXBTZEUR XXBTZEUR { get; set; }
            public XXBTZEURD XXBTZEURd { get; set; }
            public XXBTZGBP XXBTZGBP { get; set; }
            public XXBTZGBPD XXBTZGBPd { get; set; }
            public XXBTZJPY XXBTZJPY { get; set; }
            public XXBTZJPYD XXBTZJPYd { get; set; }
            public XXBTZUSD XXBTZUSD { get; set; }
            public XXBTZUSDD XXBTZUSDd { get; set; }
            public XXDGXXBT XXDGXXBT { get; set; }
            public XXLMXXBT XXLMXXBT { get; set; }
            public XXLMZEUR XXLMZEUR { get; set; }
            public XXLMZUSD XXLMZUSD { get; set; }
            public XXMRXXBT XXMRXXBT { get; set; }
            public XXMRZEUR XXMRZEUR { get; set; }
            public XXMRZUSD XXMRZUSD { get; set; }
            public XXRPXXBT XXRPXXBT { get; set; }
            public XXRPZCAD XXRPZCAD { get; set; }
            public XXRPZEUR XXRPZEUR { get; set; }
            public XXRPZJPY XXRPZJPY { get; set; }
            public XXRPZUSD XXRPZUSD { get; set; }
            public XZECXXBT XZECXXBT { get; set; }
            public XZECZEUR XZECZEUR { get; set; }
            public XZECZUSD XZECZUSD { get; set; }
        }

        public class DASHEUR
        {
            public string altname { get; set; }
            public string aclass_base { get; set; }
            public string _base { get; set; }
            public string aclass_quote { get; set; }
            public string quote { get; set; }
            public string lot { get; set; }
            public int pair_decimals { get; set; }
            public int lot_decimals { get; set; }
            public int lot_multiplier { get; set; }
            public object[] leverage_buy { get; set; }
            public object[] leverage_sell { get; set; }
            public float[][] fees { get; set; }
            public float[][] fees_maker { get; set; }
            public string fee_volume_currency { get; set; }
            public int margin_call { get; set; }
            public int margin_stop { get; set; }
        }

        public class DASHUSD
        {
            public string altname { get; set; }
            public string aclass_base { get; set; }
            public string _base { get; set; }
            public string aclass_quote { get; set; }
            public string quote { get; set; }
            public string lot { get; set; }
            public int pair_decimals { get; set; }
            public int lot_decimals { get; set; }
            public int lot_multiplier { get; set; }
            public object[] leverage_buy { get; set; }
            public object[] leverage_sell { get; set; }
            public float[][] fees { get; set; }
            public float[][] fees_maker { get; set; }
            public string fee_volume_currency { get; set; }
            public int margin_call { get; set; }
            public int margin_stop { get; set; }
        }

        public class DASHXBT
        {
            public string altname { get; set; }
            public string aclass_base { get; set; }
            public string _base { get; set; }
            public string aclass_quote { get; set; }
            public string quote { get; set; }
            public string lot { get; set; }
            public int pair_decimals { get; set; }
            public int lot_decimals { get; set; }
            public int lot_multiplier { get; set; }
            public object[] leverage_buy { get; set; }
            public object[] leverage_sell { get; set; }
            public float[][] fees { get; set; }
            public float[][] fees_maker { get; set; }
            public string fee_volume_currency { get; set; }
            public int margin_call { get; set; }
            public int margin_stop { get; set; }
        }

        public class EOSETH
        {
            public string altname { get; set; }
            public string aclass_base { get; set; }
            public string _base { get; set; }
            public string aclass_quote { get; set; }
            public string quote { get; set; }
            public string lot { get; set; }
            public int pair_decimals { get; set; }
            public int lot_decimals { get; set; }
            public int lot_multiplier { get; set; }
            public object[] leverage_buy { get; set; }
            public object[] leverage_sell { get; set; }
            public float[][] fees { get; set; }
            public float[][] fees_maker { get; set; }
            public string fee_volume_currency { get; set; }
            public int margin_call { get; set; }
            public int margin_stop { get; set; }
        }

        public class EOSEUR
        {
            public string altname { get; set; }
            public string aclass_base { get; set; }
            public string _base { get; set; }
            public string aclass_quote { get; set; }
            public string quote { get; set; }
            public string lot { get; set; }
            public int pair_decimals { get; set; }
            public int lot_decimals { get; set; }
            public int lot_multiplier { get; set; }
            public object[] leverage_buy { get; set; }
            public object[] leverage_sell { get; set; }
            public float[][] fees { get; set; }
            public float[][] fees_maker { get; set; }
            public string fee_volume_currency { get; set; }
            public int margin_call { get; set; }
            public int margin_stop { get; set; }
        }

        public class EOSUSD
        {
            public string altname { get; set; }
            public string aclass_base { get; set; }
            public string _base { get; set; }
            public string aclass_quote { get; set; }
            public string quote { get; set; }
            public string lot { get; set; }
            public int pair_decimals { get; set; }
            public int lot_decimals { get; set; }
            public int lot_multiplier { get; set; }
            public object[] leverage_buy { get; set; }
            public object[] leverage_sell { get; set; }
            public float[][] fees { get; set; }
            public float[][] fees_maker { get; set; }
            public string fee_volume_currency { get; set; }
            public int margin_call { get; set; }
            public int margin_stop { get; set; }
        }

        public class EOSXBT
        {
            public string altname { get; set; }
            public string aclass_base { get; set; }
            public string _base { get; set; }
            public string aclass_quote { get; set; }
            public string quote { get; set; }
            public string lot { get; set; }
            public int pair_decimals { get; set; }
            public int lot_decimals { get; set; }
            public int lot_multiplier { get; set; }
            public object[] leverage_buy { get; set; }
            public object[] leverage_sell { get; set; }
            public float[][] fees { get; set; }
            public float[][] fees_maker { get; set; }
            public string fee_volume_currency { get; set; }
            public int margin_call { get; set; }
            public int margin_stop { get; set; }
        }

        public class GNOETH
        {
            public string altname { get; set; }
            public string aclass_base { get; set; }
            public string _base { get; set; }
            public string aclass_quote { get; set; }
            public string quote { get; set; }
            public string lot { get; set; }
            public int pair_decimals { get; set; }
            public int lot_decimals { get; set; }
            public int lot_multiplier { get; set; }
            public object[] leverage_buy { get; set; }
            public object[] leverage_sell { get; set; }
            public float[][] fees { get; set; }
            public float[][] fees_maker { get; set; }
            public string fee_volume_currency { get; set; }
            public int margin_call { get; set; }
            public int margin_stop { get; set; }
        }

        public class GNOEUR
        {
            public string altname { get; set; }
            public string aclass_base { get; set; }
            public string _base { get; set; }
            public string aclass_quote { get; set; }
            public string quote { get; set; }
            public string lot { get; set; }
            public int pair_decimals { get; set; }
            public int lot_decimals { get; set; }
            public int lot_multiplier { get; set; }
            public object[] leverage_buy { get; set; }
            public object[] leverage_sell { get; set; }
            public float[][] fees { get; set; }
            public float[][] fees_maker { get; set; }
            public string fee_volume_currency { get; set; }
            public int margin_call { get; set; }
            public int margin_stop { get; set; }
        }

        public class GNOUSD
        {
            public string altname { get; set; }
            public string aclass_base { get; set; }
            public string _base { get; set; }
            public string aclass_quote { get; set; }
            public string quote { get; set; }
            public string lot { get; set; }
            public int pair_decimals { get; set; }
            public int lot_decimals { get; set; }
            public int lot_multiplier { get; set; }
            public object[] leverage_buy { get; set; }
            public object[] leverage_sell { get; set; }
            public float[][] fees { get; set; }
            public float[][] fees_maker { get; set; }
            public string fee_volume_currency { get; set; }
            public int margin_call { get; set; }
            public int margin_stop { get; set; }
        }

        public class GNOXBT
        {
            public string altname { get; set; }
            public string aclass_base { get; set; }
            public string _base { get; set; }
            public string aclass_quote { get; set; }
            public string quote { get; set; }
            public string lot { get; set; }
            public int pair_decimals { get; set; }
            public int lot_decimals { get; set; }
            public int lot_multiplier { get; set; }
            public object[] leverage_buy { get; set; }
            public object[] leverage_sell { get; set; }
            public float[][] fees { get; set; }
            public float[][] fees_maker { get; set; }
            public string fee_volume_currency { get; set; }
            public int margin_call { get; set; }
            public int margin_stop { get; set; }
        }

        public class USDTZUSD
        {
            public string altname { get; set; }
            public string aclass_base { get; set; }
            public string _base { get; set; }
            public string aclass_quote { get; set; }
            public string quote { get; set; }
            public string lot { get; set; }
            public int pair_decimals { get; set; }
            public int lot_decimals { get; set; }
            public int lot_multiplier { get; set; }
            public int[] leverage_buy { get; set; }
            public int[] leverage_sell { get; set; }
            public float[][] fees { get; set; }
            public float[][] fees_maker { get; set; }
            public string fee_volume_currency { get; set; }
            public int margin_call { get; set; }
            public int margin_stop { get; set; }
        }

        public class XETCXETH
        {
            public string altname { get; set; }
            public string aclass_base { get; set; }
            public string _base { get; set; }
            public string aclass_quote { get; set; }
            public string quote { get; set; }
            public string lot { get; set; }
            public int pair_decimals { get; set; }
            public int lot_decimals { get; set; }
            public int lot_multiplier { get; set; }
            public int[] leverage_buy { get; set; }
            public int[] leverage_sell { get; set; }
            public float[][] fees { get; set; }
            public float[][] fees_maker { get; set; }
            public string fee_volume_currency { get; set; }
            public int margin_call { get; set; }
            public int margin_stop { get; set; }
        }

        public class XETCXXBT
        {
            public string altname { get; set; }
            public string aclass_base { get; set; }
            public string _base { get; set; }
            public string aclass_quote { get; set; }
            public string quote { get; set; }
            public string lot { get; set; }
            public int pair_decimals { get; set; }
            public int lot_decimals { get; set; }
            public int lot_multiplier { get; set; }
            public int[] leverage_buy { get; set; }
            public int[] leverage_sell { get; set; }
            public float[][] fees { get; set; }
            public float[][] fees_maker { get; set; }
            public string fee_volume_currency { get; set; }
            public int margin_call { get; set; }
            public int margin_stop { get; set; }
        }

        public class XETCZEUR
        {
            public string altname { get; set; }
            public string aclass_base { get; set; }
            public string _base { get; set; }
            public string aclass_quote { get; set; }
            public string quote { get; set; }
            public string lot { get; set; }
            public int pair_decimals { get; set; }
            public int lot_decimals { get; set; }
            public int lot_multiplier { get; set; }
            public int[] leverage_buy { get; set; }
            public int[] leverage_sell { get; set; }
            public float[][] fees { get; set; }
            public float[][] fees_maker { get; set; }
            public string fee_volume_currency { get; set; }
            public int margin_call { get; set; }
            public int margin_stop { get; set; }
        }

        public class XETCZUSD
        {
            public string altname { get; set; }
            public string aclass_base { get; set; }
            public string _base { get; set; }
            public string aclass_quote { get; set; }
            public string quote { get; set; }
            public string lot { get; set; }
            public int pair_decimals { get; set; }
            public int lot_decimals { get; set; }
            public int lot_multiplier { get; set; }
            public int[] leverage_buy { get; set; }
            public int[] leverage_sell { get; set; }
            public float[][] fees { get; set; }
            public float[][] fees_maker { get; set; }
            public string fee_volume_currency { get; set; }
            public int margin_call { get; set; }
            public int margin_stop { get; set; }
        }

        public class XETHXXBT
        {
            public string altname { get; set; }
            public string aclass_base { get; set; }
            public string _base { get; set; }
            public string aclass_quote { get; set; }
            public string quote { get; set; }
            public string lot { get; set; }
            public int pair_decimals { get; set; }
            public int lot_decimals { get; set; }
            public int lot_multiplier { get; set; }
            public int[] leverage_buy { get; set; }
            public int[] leverage_sell { get; set; }
            public float[][] fees { get; set; }
            public float[][] fees_maker { get; set; }
            public string fee_volume_currency { get; set; }
            public int margin_call { get; set; }
            public int margin_stop { get; set; }
        }

        public class XETHXXBTD
        {
            public string altname { get; set; }
            public string aclass_base { get; set; }
            public string _base { get; set; }
            public string aclass_quote { get; set; }
            public string quote { get; set; }
            public string lot { get; set; }
            public int pair_decimals { get; set; }
            public int lot_decimals { get; set; }
            public int lot_multiplier { get; set; }
            public object[] leverage_buy { get; set; }
            public object[] leverage_sell { get; set; }
            public float[][] fees { get; set; }
            public string fee_volume_currency { get; set; }
            public int margin_call { get; set; }
            public int margin_stop { get; set; }
        }

        public class XETHZCAD
        {
            public string altname { get; set; }
            public string aclass_base { get; set; }
            public string _base { get; set; }
            public string aclass_quote { get; set; }
            public string quote { get; set; }
            public string lot { get; set; }
            public int pair_decimals { get; set; }
            public int lot_decimals { get; set; }
            public int lot_multiplier { get; set; }
            public object[] leverage_buy { get; set; }
            public object[] leverage_sell { get; set; }
            public float[][] fees { get; set; }
            public float[][] fees_maker { get; set; }
            public string fee_volume_currency { get; set; }
            public int margin_call { get; set; }
            public int margin_stop { get; set; }
        }

        public class XETHZCADD
        {
            public string altname { get; set; }
            public string aclass_base { get; set; }
            public string _base { get; set; }
            public string aclass_quote { get; set; }
            public string quote { get; set; }
            public string lot { get; set; }
            public int pair_decimals { get; set; }
            public int lot_decimals { get; set; }
            public int lot_multiplier { get; set; }
            public object[] leverage_buy { get; set; }
            public object[] leverage_sell { get; set; }
            public float[][] fees { get; set; }
            public string fee_volume_currency { get; set; }
            public int margin_call { get; set; }
            public int margin_stop { get; set; }
        }

        public class XETHZEUR
        {
            public string altname { get; set; }
            public string aclass_base { get; set; }
            public string _base { get; set; }
            public string aclass_quote { get; set; }
            public string quote { get; set; }
            public string lot { get; set; }
            public int pair_decimals { get; set; }
            public int lot_decimals { get; set; }
            public int lot_multiplier { get; set; }
            public int[] leverage_buy { get; set; }
            public int[] leverage_sell { get; set; }
            public float[][] fees { get; set; }
            public float[][] fees_maker { get; set; }
            public string fee_volume_currency { get; set; }
            public int margin_call { get; set; }
            public int margin_stop { get; set; }
        }

        public class XETHZEURD
        {
            public string altname { get; set; }
            public string aclass_base { get; set; }
            public string _base { get; set; }
            public string aclass_quote { get; set; }
            public string quote { get; set; }
            public string lot { get; set; }
            public int pair_decimals { get; set; }
            public int lot_decimals { get; set; }
            public int lot_multiplier { get; set; }
            public object[] leverage_buy { get; set; }
            public object[] leverage_sell { get; set; }
            public float[][] fees { get; set; }
            public string fee_volume_currency { get; set; }
            public int margin_call { get; set; }
            public int margin_stop { get; set; }
        }

        public class XETHZGBP
        {
            public string altname { get; set; }
            public string aclass_base { get; set; }
            public string _base { get; set; }
            public string aclass_quote { get; set; }
            public string quote { get; set; }
            public string lot { get; set; }
            public int pair_decimals { get; set; }
            public int lot_decimals { get; set; }
            public int lot_multiplier { get; set; }
            public object[] leverage_buy { get; set; }
            public object[] leverage_sell { get; set; }
            public float[][] fees { get; set; }
            public float[][] fees_maker { get; set; }
            public string fee_volume_currency { get; set; }
            public int margin_call { get; set; }
            public int margin_stop { get; set; }
        }

        public class XETHZGBPD
        {
            public string altname { get; set; }
            public string aclass_base { get; set; }
            public string _base { get; set; }
            public string aclass_quote { get; set; }
            public string quote { get; set; }
            public string lot { get; set; }
            public int pair_decimals { get; set; }
            public int lot_decimals { get; set; }
            public int lot_multiplier { get; set; }
            public object[] leverage_buy { get; set; }
            public object[] leverage_sell { get; set; }
            public float[][] fees { get; set; }
            public string fee_volume_currency { get; set; }
            public int margin_call { get; set; }
            public int margin_stop { get; set; }
        }

        public class XETHZJPY
        {
            public string altname { get; set; }
            public string aclass_base { get; set; }
            public string _base { get; set; }
            public string aclass_quote { get; set; }
            public string quote { get; set; }
            public string lot { get; set; }
            public int pair_decimals { get; set; }
            public int lot_decimals { get; set; }
            public int lot_multiplier { get; set; }
            public object[] leverage_buy { get; set; }
            public object[] leverage_sell { get; set; }
            public float[][] fees { get; set; }
            public float[][] fees_maker { get; set; }
            public string fee_volume_currency { get; set; }
            public int margin_call { get; set; }
            public int margin_stop { get; set; }
        }

        public class XETHZJPYD
        {
            public string altname { get; set; }
            public string aclass_base { get; set; }
            public string _base { get; set; }
            public string aclass_quote { get; set; }
            public string quote { get; set; }
            public string lot { get; set; }
            public int pair_decimals { get; set; }
            public int lot_decimals { get; set; }
            public int lot_multiplier { get; set; }
            public object[] leverage_buy { get; set; }
            public object[] leverage_sell { get; set; }
            public float[][] fees { get; set; }
            public string fee_volume_currency { get; set; }
            public int margin_call { get; set; }
            public int margin_stop { get; set; }
        }

        public class XETHZUSD
        {
            public string altname { get; set; }
            public string aclass_base { get; set; }
            public string _base { get; set; }
            public string aclass_quote { get; set; }
            public string quote { get; set; }
            public string lot { get; set; }
            public int pair_decimals { get; set; }
            public int lot_decimals { get; set; }
            public int lot_multiplier { get; set; }
            public int[] leverage_buy { get; set; }
            public int[] leverage_sell { get; set; }
            public float[][] fees { get; set; }
            public float[][] fees_maker { get; set; }
            public string fee_volume_currency { get; set; }
            public int margin_call { get; set; }
            public int margin_stop { get; set; }
        }

        public class XETHZUSDD
        {
            public string altname { get; set; }
            public string aclass_base { get; set; }
            public string _base { get; set; }
            public string aclass_quote { get; set; }
            public string quote { get; set; }
            public string lot { get; set; }
            public int pair_decimals { get; set; }
            public int lot_decimals { get; set; }
            public int lot_multiplier { get; set; }
            public object[] leverage_buy { get; set; }
            public object[] leverage_sell { get; set; }
            public float[][] fees { get; set; }
            public string fee_volume_currency { get; set; }
            public int margin_call { get; set; }
            public int margin_stop { get; set; }
        }

        public class XICNXETH
        {
            public string altname { get; set; }
            public string aclass_base { get; set; }
            public string _base { get; set; }
            public string aclass_quote { get; set; }
            public string quote { get; set; }
            public string lot { get; set; }
            public int pair_decimals { get; set; }
            public int lot_decimals { get; set; }
            public int lot_multiplier { get; set; }
            public object[] leverage_buy { get; set; }
            public object[] leverage_sell { get; set; }
            public float[][] fees { get; set; }
            public float[][] fees_maker { get; set; }
            public string fee_volume_currency { get; set; }
            public int margin_call { get; set; }
            public int margin_stop { get; set; }
        }

        public class XICNXXBT
        {
            public string altname { get; set; }
            public string aclass_base { get; set; }
            public string _base { get; set; }
            public string aclass_quote { get; set; }
            public string quote { get; set; }
            public string lot { get; set; }
            public int pair_decimals { get; set; }
            public int lot_decimals { get; set; }
            public int lot_multiplier { get; set; }
            public object[] leverage_buy { get; set; }
            public object[] leverage_sell { get; set; }
            public float[][] fees { get; set; }
            public float[][] fees_maker { get; set; }
            public string fee_volume_currency { get; set; }
            public int margin_call { get; set; }
            public int margin_stop { get; set; }
        }

        public class XLTCXXBT
        {
            public string altname { get; set; }
            public string aclass_base { get; set; }
            public string _base { get; set; }
            public string aclass_quote { get; set; }
            public string quote { get; set; }
            public string lot { get; set; }
            public int pair_decimals { get; set; }
            public int lot_decimals { get; set; }
            public int lot_multiplier { get; set; }
            public object[] leverage_buy { get; set; }
            public object[] leverage_sell { get; set; }
            public float[][] fees { get; set; }
            public float[][] fees_maker { get; set; }
            public string fee_volume_currency { get; set; }
            public int margin_call { get; set; }
            public int margin_stop { get; set; }
        }

        public class XLTCZEUR
        {
            public string altname { get; set; }
            public string aclass_base { get; set; }
            public string _base { get; set; }
            public string aclass_quote { get; set; }
            public string quote { get; set; }
            public string lot { get; set; }
            public int pair_decimals { get; set; }
            public int lot_decimals { get; set; }
            public int lot_multiplier { get; set; }
            public object[] leverage_buy { get; set; }
            public object[] leverage_sell { get; set; }
            public float[][] fees { get; set; }
            public float[][] fees_maker { get; set; }
            public string fee_volume_currency { get; set; }
            public int margin_call { get; set; }
            public int margin_stop { get; set; }
        }

        public class XLTCZUSD
        {
            public string altname { get; set; }
            public string aclass_base { get; set; }
            public string _base { get; set; }
            public string aclass_quote { get; set; }
            public string quote { get; set; }
            public string lot { get; set; }
            public int pair_decimals { get; set; }
            public int lot_decimals { get; set; }
            public int lot_multiplier { get; set; }
            public object[] leverage_buy { get; set; }
            public object[] leverage_sell { get; set; }
            public float[][] fees { get; set; }
            public float[][] fees_maker { get; set; }
            public string fee_volume_currency { get; set; }
            public int margin_call { get; set; }
            public int margin_stop { get; set; }
        }

        public class XMLNXETH
        {
            public string altname { get; set; }
            public string aclass_base { get; set; }
            public string _base { get; set; }
            public string aclass_quote { get; set; }
            public string quote { get; set; }
            public string lot { get; set; }
            public int pair_decimals { get; set; }
            public int lot_decimals { get; set; }
            public int lot_multiplier { get; set; }
            public object[] leverage_buy { get; set; }
            public object[] leverage_sell { get; set; }
            public float[][] fees { get; set; }
            public float[][] fees_maker { get; set; }
            public string fee_volume_currency { get; set; }
            public int margin_call { get; set; }
            public int margin_stop { get; set; }
        }

        public class XMLNXXBT
        {
            public string altname { get; set; }
            public string aclass_base { get; set; }
            public string _base { get; set; }
            public string aclass_quote { get; set; }
            public string quote { get; set; }
            public string lot { get; set; }
            public int pair_decimals { get; set; }
            public int lot_decimals { get; set; }
            public int lot_multiplier { get; set; }
            public object[] leverage_buy { get; set; }
            public object[] leverage_sell { get; set; }
            public float[][] fees { get; set; }
            public float[][] fees_maker { get; set; }
            public string fee_volume_currency { get; set; }
            public int margin_call { get; set; }
            public int margin_stop { get; set; }
        }

        public class XREPXETH
        {
            public string altname { get; set; }
            public string aclass_base { get; set; }
            public string _base { get; set; }
            public string aclass_quote { get; set; }
            public string quote { get; set; }
            public string lot { get; set; }
            public int pair_decimals { get; set; }
            public int lot_decimals { get; set; }
            public int lot_multiplier { get; set; }
            public int[] leverage_buy { get; set; }
            public int[] leverage_sell { get; set; }
            public float[][] fees { get; set; }
            public float[][] fees_maker { get; set; }
            public string fee_volume_currency { get; set; }
            public int margin_call { get; set; }
            public int margin_stop { get; set; }
        }

        public class XREPXXBT
        {
            public string altname { get; set; }
            public string aclass_base { get; set; }
            public string _base { get; set; }
            public string aclass_quote { get; set; }
            public string quote { get; set; }
            public string lot { get; set; }
            public int pair_decimals { get; set; }
            public int lot_decimals { get; set; }
            public int lot_multiplier { get; set; }
            public int[] leverage_buy { get; set; }
            public int[] leverage_sell { get; set; }
            public float[][] fees { get; set; }
            public float[][] fees_maker { get; set; }
            public string fee_volume_currency { get; set; }
            public int margin_call { get; set; }
            public int margin_stop { get; set; }
        }

        public class XREPZEUR
        {
            public string altname { get; set; }
            public string aclass_base { get; set; }
            public string _base { get; set; }
            public string aclass_quote { get; set; }
            public string quote { get; set; }
            public string lot { get; set; }
            public int pair_decimals { get; set; }
            public int lot_decimals { get; set; }
            public int lot_multiplier { get; set; }
            public int[] leverage_buy { get; set; }
            public int[] leverage_sell { get; set; }
            public float[][] fees { get; set; }
            public float[][] fees_maker { get; set; }
            public string fee_volume_currency { get; set; }
            public int margin_call { get; set; }
            public int margin_stop { get; set; }
        }

        public class XREPZUSD
        {
            public string altname { get; set; }
            public string aclass_base { get; set; }
            public string _base { get; set; }
            public string aclass_quote { get; set; }
            public string quote { get; set; }
            public string lot { get; set; }
            public int pair_decimals { get; set; }
            public int lot_decimals { get; set; }
            public int lot_multiplier { get; set; }
            public object[] leverage_buy { get; set; }
            public object[] leverage_sell { get; set; }
            public float[][] fees { get; set; }
            public float[][] fees_maker { get; set; }
            public string fee_volume_currency { get; set; }
            public int margin_call { get; set; }
            public int margin_stop { get; set; }
        }

        public class XXBTZCAD
        {
            public string altname { get; set; }
            public string aclass_base { get; set; }
            public string _base { get; set; }
            public string aclass_quote { get; set; }
            public string quote { get; set; }
            public string lot { get; set; }
            public int pair_decimals { get; set; }
            public int lot_decimals { get; set; }
            public int lot_multiplier { get; set; }
            public object[] leverage_buy { get; set; }
            public object[] leverage_sell { get; set; }
            public float[][] fees { get; set; }
            public float[][] fees_maker { get; set; }
            public string fee_volume_currency { get; set; }
            public int margin_call { get; set; }
            public int margin_stop { get; set; }
        }

        public class XXBTZCADD
        {
            public string altname { get; set; }
            public string aclass_base { get; set; }
            public string _base { get; set; }
            public string aclass_quote { get; set; }
            public string quote { get; set; }
            public string lot { get; set; }
            public int pair_decimals { get; set; }
            public int lot_decimals { get; set; }
            public int lot_multiplier { get; set; }
            public object[] leverage_buy { get; set; }
            public object[] leverage_sell { get; set; }
            public float[][] fees { get; set; }
            public string fee_volume_currency { get; set; }
            public int margin_call { get; set; }
            public int margin_stop { get; set; }
        }

        public class XXBTZEUR
        {
            public string altname { get; set; }
            public string aclass_base { get; set; }
            public string _base { get; set; }
            public string aclass_quote { get; set; }
            public string quote { get; set; }
            public string lot { get; set; }
            public int pair_decimals { get; set; }
            public int lot_decimals { get; set; }
            public int lot_multiplier { get; set; }
            public int[] leverage_buy { get; set; }
            public int[] leverage_sell { get; set; }
            public float[][] fees { get; set; }
            public float[][] fees_maker { get; set; }
            public string fee_volume_currency { get; set; }
            public int margin_call { get; set; }
            public int margin_stop { get; set; }
        }

        public class XXBTZEURD
        {
            public string altname { get; set; }
            public string aclass_base { get; set; }
            public string _base { get; set; }
            public string aclass_quote { get; set; }
            public string quote { get; set; }
            public string lot { get; set; }
            public int pair_decimals { get; set; }
            public int lot_decimals { get; set; }
            public int lot_multiplier { get; set; }
            public object[] leverage_buy { get; set; }
            public object[] leverage_sell { get; set; }
            public float[][] fees { get; set; }
            public string fee_volume_currency { get; set; }
            public int margin_call { get; set; }
            public int margin_stop { get; set; }
        }

        public class XXBTZGBP
        {
            public string altname { get; set; }
            public string aclass_base { get; set; }
            public string _base { get; set; }
            public string aclass_quote { get; set; }
            public string quote { get; set; }
            public string lot { get; set; }
            public int pair_decimals { get; set; }
            public int lot_decimals { get; set; }
            public int lot_multiplier { get; set; }
            public object[] leverage_buy { get; set; }
            public object[] leverage_sell { get; set; }
            public float[][] fees { get; set; }
            public float[][] fees_maker { get; set; }
            public string fee_volume_currency { get; set; }
            public int margin_call { get; set; }
            public int margin_stop { get; set; }
        }

        public class XXBTZGBPD
        {
            public string altname { get; set; }
            public string aclass_base { get; set; }
            public string _base { get; set; }
            public string aclass_quote { get; set; }
            public string quote { get; set; }
            public string lot { get; set; }
            public int pair_decimals { get; set; }
            public int lot_decimals { get; set; }
            public int lot_multiplier { get; set; }
            public object[] leverage_buy { get; set; }
            public object[] leverage_sell { get; set; }
            public float[][] fees { get; set; }
            public string fee_volume_currency { get; set; }
            public int margin_call { get; set; }
            public int margin_stop { get; set; }
        }

        public class XXBTZJPY
        {
            public string altname { get; set; }
            public string aclass_base { get; set; }
            public string _base { get; set; }
            public string aclass_quote { get; set; }
            public string quote { get; set; }
            public string lot { get; set; }
            public int pair_decimals { get; set; }
            public int lot_decimals { get; set; }
            public int lot_multiplier { get; set; }
            public object[] leverage_buy { get; set; }
            public object[] leverage_sell { get; set; }
            public float[][] fees { get; set; }
            public float[][] fees_maker { get; set; }
            public string fee_volume_currency { get; set; }
            public int margin_call { get; set; }
            public int margin_stop { get; set; }
        }

        public class XXBTZJPYD
        {
            public string altname { get; set; }
            public string aclass_base { get; set; }
            public string _base { get; set; }
            public string aclass_quote { get; set; }
            public string quote { get; set; }
            public string lot { get; set; }
            public int pair_decimals { get; set; }
            public int lot_decimals { get; set; }
            public int lot_multiplier { get; set; }
            public object[] leverage_buy { get; set; }
            public object[] leverage_sell { get; set; }
            public float[][] fees { get; set; }
            public string fee_volume_currency { get; set; }
            public int margin_call { get; set; }
            public int margin_stop { get; set; }
        }

        public class XXBTZUSD
        {
            public string altname { get; set; }
            public string aclass_base { get; set; }
            public string _base { get; set; }
            public string aclass_quote { get; set; }
            public string quote { get; set; }
            public string lot { get; set; }
            public int pair_decimals { get; set; }
            public int lot_decimals { get; set; }
            public int lot_multiplier { get; set; }
            public int[] leverage_buy { get; set; }
            public int[] leverage_sell { get; set; }
            public float[][] fees { get; set; }
            public float[][] fees_maker { get; set; }
            public string fee_volume_currency { get; set; }
            public int margin_call { get; set; }
            public int margin_stop { get; set; }
        }

        public class XXBTZUSDD
        {
            public string altname { get; set; }
            public string aclass_base { get; set; }
            public string _base { get; set; }
            public string aclass_quote { get; set; }
            public string quote { get; set; }
            public string lot { get; set; }
            public int pair_decimals { get; set; }
            public int lot_decimals { get; set; }
            public int lot_multiplier { get; set; }
            public object[] leverage_buy { get; set; }
            public object[] leverage_sell { get; set; }
            public float[][] fees { get; set; }
            public string fee_volume_currency { get; set; }
            public int margin_call { get; set; }
            public int margin_stop { get; set; }
        }

        public class XXDGXXBT
        {
            public string altname { get; set; }
            public string aclass_base { get; set; }
            public string _base { get; set; }
            public string aclass_quote { get; set; }
            public string quote { get; set; }
            public string lot { get; set; }
            public int pair_decimals { get; set; }
            public int lot_decimals { get; set; }
            public int lot_multiplier { get; set; }
            public object[] leverage_buy { get; set; }
            public object[] leverage_sell { get; set; }
            public float[][] fees { get; set; }
            public float[][] fees_maker { get; set; }
            public string fee_volume_currency { get; set; }
            public int margin_call { get; set; }
            public int margin_stop { get; set; }
        }

        public class XXLMXXBT
        {
            public string altname { get; set; }
            public string aclass_base { get; set; }
            public string _base { get; set; }
            public string aclass_quote { get; set; }
            public string quote { get; set; }
            public string lot { get; set; }
            public int pair_decimals { get; set; }
            public int lot_decimals { get; set; }
            public int lot_multiplier { get; set; }
            public object[] leverage_buy { get; set; }
            public object[] leverage_sell { get; set; }
            public float[][] fees { get; set; }
            public float[][] fees_maker { get; set; }
            public string fee_volume_currency { get; set; }
            public int margin_call { get; set; }
            public int margin_stop { get; set; }
        }

        public class XXLMZEUR
        {
            public string altname { get; set; }
            public string aclass_base { get; set; }
            public string _base { get; set; }
            public string aclass_quote { get; set; }
            public string quote { get; set; }
            public string lot { get; set; }
            public int pair_decimals { get; set; }
            public int lot_decimals { get; set; }
            public int lot_multiplier { get; set; }
            public object[] leverage_buy { get; set; }
            public object[] leverage_sell { get; set; }
            public float[][] fees { get; set; }
            public float[][] fees_maker { get; set; }
            public string fee_volume_currency { get; set; }
            public int margin_call { get; set; }
            public int margin_stop { get; set; }
        }

        public class XXLMZUSD
        {
            public string altname { get; set; }
            public string aclass_base { get; set; }
            public string _base { get; set; }
            public string aclass_quote { get; set; }
            public string quote { get; set; }
            public string lot { get; set; }
            public int pair_decimals { get; set; }
            public int lot_decimals { get; set; }
            public int lot_multiplier { get; set; }
            public object[] leverage_buy { get; set; }
            public object[] leverage_sell { get; set; }
            public float[][] fees { get; set; }
            public float[][] fees_maker { get; set; }
            public string fee_volume_currency { get; set; }
            public int margin_call { get; set; }
            public int margin_stop { get; set; }
        }

        public class XXMRXXBT
        {
            public string altname { get; set; }
            public string aclass_base { get; set; }
            public string _base { get; set; }
            public string aclass_quote { get; set; }
            public string quote { get; set; }
            public string lot { get; set; }
            public int pair_decimals { get; set; }
            public int lot_decimals { get; set; }
            public int lot_multiplier { get; set; }
            public int[] leverage_buy { get; set; }
            public int[] leverage_sell { get; set; }
            public float[][] fees { get; set; }
            public float[][] fees_maker { get; set; }
            public string fee_volume_currency { get; set; }
            public int margin_call { get; set; }
            public int margin_stop { get; set; }
        }

        public class XXMRZEUR
        {
            public string altname { get; set; }
            public string aclass_base { get; set; }
            public string _base { get; set; }
            public string aclass_quote { get; set; }
            public string quote { get; set; }
            public string lot { get; set; }
            public int pair_decimals { get; set; }
            public int lot_decimals { get; set; }
            public int lot_multiplier { get; set; }
            public int[] leverage_buy { get; set; }
            public int[] leverage_sell { get; set; }
            public float[][] fees { get; set; }
            public float[][] fees_maker { get; set; }
            public string fee_volume_currency { get; set; }
            public int margin_call { get; set; }
            public int margin_stop { get; set; }
        }

        public class XXMRZUSD
        {
            public string altname { get; set; }
            public string aclass_base { get; set; }
            public string _base { get; set; }
            public string aclass_quote { get; set; }
            public string quote { get; set; }
            public string lot { get; set; }
            public int pair_decimals { get; set; }
            public int lot_decimals { get; set; }
            public int lot_multiplier { get; set; }
            public int[] leverage_buy { get; set; }
            public int[] leverage_sell { get; set; }
            public float[][] fees { get; set; }
            public float[][] fees_maker { get; set; }
            public string fee_volume_currency { get; set; }
            public int margin_call { get; set; }
            public int margin_stop { get; set; }
        }

        public class XXRPXXBT
        {
            public string altname { get; set; }
            public string aclass_base { get; set; }
            public string _base { get; set; }
            public string aclass_quote { get; set; }
            public string quote { get; set; }
            public string lot { get; set; }
            public int pair_decimals { get; set; }
            public int lot_decimals { get; set; }
            public int lot_multiplier { get; set; }
            public object[] leverage_buy { get; set; }
            public object[] leverage_sell { get; set; }
            public float[][] fees { get; set; }
            public float[][] fees_maker { get; set; }
            public string fee_volume_currency { get; set; }
            public int margin_call { get; set; }
            public int margin_stop { get; set; }
        }

        public class XXRPZCAD
        {
            public string altname { get; set; }
            public string aclass_base { get; set; }
            public string _base { get; set; }
            public string aclass_quote { get; set; }
            public string quote { get; set; }
            public string lot { get; set; }
            public int pair_decimals { get; set; }
            public int lot_decimals { get; set; }
            public int lot_multiplier { get; set; }
            public object[] leverage_buy { get; set; }
            public object[] leverage_sell { get; set; }
            public float[][] fees { get; set; }
            public float[][] fees_maker { get; set; }
            public string fee_volume_currency { get; set; }
            public int margin_call { get; set; }
            public int margin_stop { get; set; }
        }

        public class XXRPZEUR
        {
            public string altname { get; set; }
            public string aclass_base { get; set; }
            public string _base { get; set; }
            public string aclass_quote { get; set; }
            public string quote { get; set; }
            public string lot { get; set; }
            public int pair_decimals { get; set; }
            public int lot_decimals { get; set; }
            public int lot_multiplier { get; set; }
            public object[] leverage_buy { get; set; }
            public object[] leverage_sell { get; set; }
            public float[][] fees { get; set; }
            public float[][] fees_maker { get; set; }
            public string fee_volume_currency { get; set; }
            public int margin_call { get; set; }
            public int margin_stop { get; set; }
        }

        public class XXRPZJPY
        {
            public string altname { get; set; }
            public string aclass_base { get; set; }
            public string _base { get; set; }
            public string aclass_quote { get; set; }
            public string quote { get; set; }
            public string lot { get; set; }
            public int pair_decimals { get; set; }
            public int lot_decimals { get; set; }
            public int lot_multiplier { get; set; }
            public object[] leverage_buy { get; set; }
            public object[] leverage_sell { get; set; }
            public float[][] fees { get; set; }
            public float[][] fees_maker { get; set; }
            public string fee_volume_currency { get; set; }
            public int margin_call { get; set; }
            public int margin_stop { get; set; }
        }

        public class XXRPZUSD
        {
            public string altname { get; set; }
            public string aclass_base { get; set; }
            public string _base { get; set; }
            public string aclass_quote { get; set; }
            public string quote { get; set; }
            public string lot { get; set; }
            public int pair_decimals { get; set; }
            public int lot_decimals { get; set; }
            public int lot_multiplier { get; set; }
            public object[] leverage_buy { get; set; }
            public object[] leverage_sell { get; set; }
            public float[][] fees { get; set; }
            public float[][] fees_maker { get; set; }
            public string fee_volume_currency { get; set; }
            public int margin_call { get; set; }
            public int margin_stop { get; set; }
        }

        public class XZECXXBT
        {
            public string altname { get; set; }
            public string aclass_base { get; set; }
            public string _base { get; set; }
            public string aclass_quote { get; set; }
            public string quote { get; set; }
            public string lot { get; set; }
            public int pair_decimals { get; set; }
            public int lot_decimals { get; set; }
            public int lot_multiplier { get; set; }
            public object[] leverage_buy { get; set; }
            public object[] leverage_sell { get; set; }
            public float[][] fees { get; set; }
            public float[][] fees_maker { get; set; }
            public string fee_volume_currency { get; set; }
            public int margin_call { get; set; }
            public int margin_stop { get; set; }
        }

        public class XZECZEUR
        {
            public string altname { get; set; }
            public string aclass_base { get; set; }
            public string _base { get; set; }
            public string aclass_quote { get; set; }
            public string quote { get; set; }
            public string lot { get; set; }
            public int pair_decimals { get; set; }
            public int lot_decimals { get; set; }
            public int lot_multiplier { get; set; }
            public object[] leverage_buy { get; set; }
            public object[] leverage_sell { get; set; }
            public float[][] fees { get; set; }
            public float[][] fees_maker { get; set; }
            public string fee_volume_currency { get; set; }
            public int margin_call { get; set; }
            public int margin_stop { get; set; }
        }

        public class XZECZUSD
        {
            public string altname { get; set; }
            public string aclass_base { get; set; }
            public string _base { get; set; }
            public string aclass_quote { get; set; }
            public string quote { get; set; }
            public string lot { get; set; }
            public int pair_decimals { get; set; }
            public int lot_decimals { get; set; }
            public int lot_multiplier { get; set; }
            public object[] leverage_buy { get; set; }
            public object[] leverage_sell { get; set; }
            public float[][] fees { get; set; }
            public float[][] fees_maker { get; set; }
            public string fee_volume_currency { get; set; }
            public int margin_call { get; set; }
            public int margin_stop { get; set; }
        }

    }
}
