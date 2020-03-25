use aws_lambda_events::event::cloudwatch_events::CloudWatchEvent;
use futures::future::TryFutureExt;
use lambda::handler_fn;
use log::{error, info};
use serde_json::json;
use thiserror::Error;

/// All the errors that can happen in this lambda
#[derive(Debug, Error)]
enum Errors {
    #[error("failed to init logger")]
    SetLogger(#[from] log::SetLoggerError),
}

type Result<T, E = Errors> = std::result::Result<T, E>;

#[tokio::main]
async fn main() -> Result<(), Box<dyn std::error::Error + Send + Sync + 'static>> {
    simple_logger::init_with_level(log::Level::Info).map_err(Errors::SetLogger)?;

    let func = handler_fn(|event| handler(event).inspect_err(|err| error!("{}", json!({ "error": err.to_string() }))));
    lambda::run(func).await?;
    Ok(())
}

async fn handler(event: CloudWatchEvent) -> Result<()> {
    info!("Hello World: {:?}", &event);
    Ok(())
}
